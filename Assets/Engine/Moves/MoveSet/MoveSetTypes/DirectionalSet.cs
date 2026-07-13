using System.Collections;
using System.Collections.Generic;
using TFraese;
using UnityEngine;

/// <summary>
/// A child class of MoveSet designed to hold pieces with color dependent
/// move vectors. Pieces act as leaping pieces for values of f, l.
/// </summary>
public class DirectionalSet : MoveSet
{
	// Store 
	public List<int> forwardOffsets;
	public List<int> lateralOffsets;

	public List<Convolution> convolutions;

	public DirectionalSet(List<int> forwardOffsets, List<int> lateralOffsets, List<Condition> conditions=null)
	{
		base.conditions = conditions;
		base.setType = SetType.Directional; // for identifying child class type
		this.forwardOffsets = forwardOffsets; // #'s of squares can move forward
		this.lateralOffsets = lateralOffsets; // #'s number of squares can move sideways
		this.convolutions = new List<Convolution>(); // initialize to empty as a fallback
	}
	public override void Generate(Game game)
	{
		// Call base virtual function to initialize the moveset list
		base.Generate(game);
		BoardLayout layout = game.variant.boardLayout;

		foreach (int f in forwardOffsets)
		{
			// Get which axes are forward from the board layout
			int[] forwards = layout.GetAxes(Axes.Forward);

			// Reserve space for our output array
			int[][] vectors = null;
			if (lateralOffsets == null || lateralOffsets.Count == 0)
			{
				// Generate the set of vectors that are scalar multiples of the supplied
				// forward directions, of a magnitude the piece is allowed to travel
				vectors = MoveGeneration.Directionals(f, forwards);
			}
			else
			{
				// Generate the set of vectors that are sums of the forward and lateral
				// offsets specified.
				foreach (int l in lateralOffsets)
				{
					// TODO Transpose based on piece... color... i need a seperate place to store them.
					int[] laterals = layout.GetAxes(Axes.Lateral);
					vectors = MoveGeneration.Directionals(f, forwards, l, laterals);
				}
			}
			// If we generated any vectors, add them to the vector list
			if (vectors != null)
			{
				foreach (int[] v in vectors)
				{
					this.NVectors.Add(new NVector(v));
				}
			}
		}
	}

	// TODO: Determine if we want to just add this to the constructor once we get
	// around to actually calling these methods.
	public void SetConvolutions(List<Convolution> convolutions)
	{
		this.convolutions = convolutions;
	}

	// Even though we can access the multiverse through the game object, we may
	// want to be able to run logic on a buffer multiverse for ai or network or
	// something.

	// TODO: See how much of this can be factored out into the parent class.
	public override List<MoveUX> ConstructMoves(Piece piece, HyperVector coordinate, Multiverse multiverse)
	{
		Axes[] axes = game.variant.boardLayout.axesTypes;
		bool flip = piece.color == 1;
		Convolution convolution = new Convolution();

		int color = piece.color;
		List<MoveUX> result = new List<MoveUX>();
		foreach (HyperVector v in base.hyperVectors)
		{
			// TODO: Clean this up. Storing axes types in a split data type like
			// hypervectors might help.
			HyperVector convoluted = new HyperVector(v);
			int[] subArray = convoluted.subVector.array;
			int[] convArray = convolution.Convolute(subArray, axes, flip);
			convoluted.subVector = new NVector(subArray);

			// TODO: Factor out the following logic. The convolution here is actually
			// the only pawn specific logic so far. See MoveSet.cs implementation for
			// more details.
			HyperVector previous;
			HyperVector sentinel = new HyperVector(coordinate);
			// TODO: Initialize or default range to int.MaxValue
			int maxStep = range == -1 ? int.MaxValue : range;
			for (int i = 0; i < maxStep; i++)
			{
				// TODO: Do a safety prune of base.hyperVectors to ensure a zero vector
				// never causes an infinite loop
				// (foreshadowing is a narrative device...)
				previous = sentinel;
				sentinel = sentinel + convoluted;
				if (!multiverse.InBounds(sentinel)) break;

				Piece target = multiverse.GetPiece(sentinel);
				if (target == null || target.isGhost)
				{
					MoveUX move = new MoveUX(piece, coordinate, sentinel);
					if (CheckConditions(piece, sentinel, target, multiverse))
					{
						result.Add(move);
					}
					continue;
				}
				if (target.color != piece.color)
				{
					MoveUX move = new MoveUX(piece, coordinate, sentinel);
					result.Add(move);
					if (CheckConditions(piece, sentinel, target, multiverse))
					{
						result.Add(move);
					}
					break;
				}
				if (target.color == piece.color)
				{
					break;
				}
			}
		}
		return result;
	}
}
