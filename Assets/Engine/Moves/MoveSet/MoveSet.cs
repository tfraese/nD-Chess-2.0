using System.Collections;
using System.Collections.Generic;
using TFraese;
using UnityEngine;

public enum SetType
{
	Container,
	Rider,
	Permutable,
	Directional,
	Explicit,
}

/*
 * A moveset is a container class for movement vectors, as well as conditions
 * and special information needed to generate the moves at runtime. A Move
 * Vector is an incomplete component of a move. The MoveUX class contains
 * information required to display and prompt the available moves to a player
 * and the MoveInternal class will hold information required to perform and
 * reverse moves in-engine.
 */
public class MoveSet
{
	// We need the game reference to get various settings and informaation about
	// the layout here.
	protected Game game;

	// Enum for child class identification
	// TODO: Maybe stop doing this as a practice and just attempt type casts
	//		 I'm sure the compiler just does this behind the scenes anyway
	public SetType setType;

	// conditions should be an object so we can pass needed information to
	// specific condition, and that condition can tell us if it p assed
	protected List<Condition> conditions;

	public List<NVector> NVectors;
	public List<HyperVector> hyperVectors;

	// TODO: Change this to default to int.MaxValue for riders
	protected int range { get; set; } = -1;

	/// <summary>
	/// Given a board layout, generate the movement vectors for a moveset
	/// within a particular game. The base virtual function initializes
	/// the list of move vectors and should always be called first within
	/// overrides.
	/// </summary>
	public virtual void Generate(Game game)
	{
		if (NVectors == null) { NVectors = new List<NVector>(); }
		this.game = game;
	}

	/// <summary>
	/// Splices the NVector's first prefixLength elements into the hyper component
	/// and the remaining into the sub component. Sorts Moves into local and
	/// multiversal as well.
	/// </summary>
	public virtual void Hyperize(int prefixLength)
	{
		hyperVectors = new List<HyperVector>();
		if (prefixLength > 0)
		{
			foreach (NVector v in NVectors)
			{
				int[] hyper = new int[prefixLength];
				System.Array.Copy(v.array, hyper, prefixLength);
				int[] sub = new int[v.array.Length - prefixLength];
				System.Array.Copy(v.array, prefixLength, sub, 0, sub.Length);

				// TODO: Figure out cleaner solution for this
				bool isLocal = true;
				for (int i = 0; i < hyper.Length; i++)
				{
					// if any of the hyper components are non-zero then flag it as non-local
					if (hyper[i] != 0) isLocal = false;
				}
				if (isLocal)
				{
					HyperVector hVectory = new HyperVector(sub);
					hyperVectors.Add(hVectory);
				}
				else
				{
					HyperVector hyperVector = new HyperVector(hyper, sub);
					hyperVectors.Add(hyperVector);
				}
			}
		}
		else
		{
			// TODO: Determine if this is neccesary or if we're just going to use NVectors in
			// singularity overloads to make the distinctions for us.
			foreach (NVector v in NVectors)
			{
				hyperVectors.Add(new HyperVector(null, v));
			}
		}
	}

	/// <summary>
	/// Given a set of information, construct the MoveUX's needed to actually
	/// fully encode and execute a move.
	/// </summary>
	public virtual List<MoveUX> ConstructMoves(
			Piece piece, // Piece reference for color and has moved info
			HyperVector coordinate, // piece doesnt store this
			Multiverse multiverse // multiverse state
		)
	{
		List<MoveUX> result = new List<MoveUX>();
		foreach (HyperVector v in hyperVectors)
		{
			// Store a previous in case we need to leave ghosts.
			// TODO: fully impelement this.
			HyperVector previous;
			HyperVector sentinel = new HyperVector(coordinate);
			// TODO: Initialize or default range to int.MaxValue
			int maxStep = range == -1 ? int.MaxValue : range;
			for (int i = 0; i < maxStep; i++)
			{
				// TODO: Do a safety prune of base.hyperVectors to ensure a zero vector
				// never causes an infinite loop
				// (foreshadowing is a narrative device...)
				
				// Perform the raycast of pieces
				previous = sentinel;
				sentinel = sentinel + v;
				if (!multiverse.InBounds(sentinel)) break;

				// TODO: The only extra functionality needed for directionals is the convolution of
				// the offset vector. Evaluate what pieces of this can be factored out to
				// cut down on repeat code. En Passant and Castling are going to use their own
				// MoveSet child class.
				Piece target = multiverse.GetPiece(sentinel);
				
				// Handle empty spaces
				if (target == null || target.isGhost)
				{
					// Construct moves regarding and add if conditions are met
					MoveUX move = new MoveUX(piece, coordinate, sentinel);
					if (CheckConditions(piece, sentinel, target, multiverse))
					{
						result.Add(move);
					}
					continue;
				}
				// Repeat the process for captures.
				if (target.color != piece.color)
				{
					MoveUX move = new MoveUX(piece, coordinate, sentinel);
					if (CheckConditions(piece, sentinel, target, multiverse))
					{
						result.Add(move);
					}
					break;
				}
				// if we hit a piece of the same color, bail out. Note that if that
				// piece were a ghost piece, we would have caught it earlier and continued
				// on with the loop anyway.
				if (target.color == piece.color)
				{
					break;
				}
			}
		}
		return result;
	}

	/// <summary>
	/// Check the movesets conditions given piece information. Note that while we
	/// do have a reference to the game in each moveset, we don't grab the
	/// multiverse from there in case we want to apply it to dummy or proxy ones.
	/// </summary>
	public bool CheckConditions(Piece from, HyperVector hyperVector, Piece to, Multiverse multiverse)
	{
		if (conditions == null) return true;
		foreach (var condition in conditions)
		{
			if (!condition.IsSatisfied(from, hyperVector, to, multiverse))
			{
				return false;
			}
		}
		return true;
	}

	// Clear all generated moves.
	public virtual void Clear()
	{
		NVectors?.Clear();	
	}


}
