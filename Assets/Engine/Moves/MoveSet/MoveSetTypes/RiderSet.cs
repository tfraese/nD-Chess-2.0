using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A child class of MoveSet designed to hold pieces that move in a number of
/// directions as once. Generates sliding pieces
/// </summary>
public class RiderSet : MoveSet
{
	public List<int> agonals;
	public RiderSet(List<int> agonals, List<Condition> conditions=null)
	{
		base.conditions = conditions;
		base.setType = SetType.Rider; // for identifying child class type
		this.agonals = agonals; // #'s of directions piece can move at a time
	}
	public override void Generate(BoardLayout layout)
	{
		// Call base virtual function to initialize the moveset list
		base.Generate(layout);

		// Calculate the dimensionality of the piece
		int n = layout.dimensions.Length;
		foreach (int c in agonals)
		{
			// if we have negative n or n out of bounds skip this element
			if (c <= 0 || c > n)
			{
				continue;
			}

			// if we dont generate any rider directions skip this element.
			// Should never happen but just in case
			int[][] riderDirections = MoveGeneration.RiderDirections(n, c);
			if (riderDirections == null ||  riderDirections.Length == 0)
			{
				continue;
			}

			// Add the vectors to the NVector list
			foreach (int[] vector in riderDirections)
			{
				NVectors.Add(new NVector(vector));
			}
		}
	}
}
