using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A child class of MoveSet designed to hold pieces with color dependent
/// move vectors. Pieces act as leaping pieces that move in permutations
/// of the offsets specified.
/// </summary>
public class PermutableSet : MoveSet
{
	public List<int> permutables;
	public PermutableSet(List<int> permutables, List<Condition> conditions = null)
	{
		base.conditions = conditions;
		base.setType = SetType.Permutable; // for identifying child class type
		this.permutables = permutables; // non-zero components to permute
	}
	public override void Generate(BoardLayout layout)
	{
		// Call base virtual function to initialize the moveset list
		base.Generate(layout);

		// Calculate the dimensionality of the move vectors to generate, as well as
		// the number of non-zero components to use
		int n = layout.dimensions.Length;
		int c = permutables.Count;

		bool valid = true;
		// if we have negative n or n out of bounds skip this element
		if (c <= 0 || c > n)
		{
			valid = false;
		}

		// if we dont generate any permutable directions skip this element.
		// Should never happen but just in case
		int[][] permutableSet = MoveGeneration.Permutables(n, permutables.ToArray());
		if (permutableSet == null || permutableSet.Length == 0)
		{
			valid = false;
		}

		// if all goes according to plan add the offsets to the NVector list
		if (valid)
		{
			foreach (int[] perm in permutableSet)
			{
				NVectors.Add(new NVector(perm));
			}
		}
	}
}
