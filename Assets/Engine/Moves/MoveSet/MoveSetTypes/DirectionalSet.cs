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
	public List<int> forwardOffsets;
	public List<int> lateralOffsets;

	public DirectionalSet(List<int> forwardOffsets, List<int> lateralOffsets, List<Condition> conditions=null)
	{
		base.conditions = conditions;
		base.setType = SetType.Directional; // for identifying child class type
		this.forwardOffsets = forwardOffsets; // #'s of squares can move forward
		this.lateralOffsets = lateralOffsets; // #'s number of squares can move sideways
	}
	public override void Generate(BoardLayout layout)
	{
		// Call base virtual function to initialize the moveset list
		base.Generate(layout);

		foreach(int f in forwardOffsets)
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
}
