using System.Collections;
using System.Collections.Generic;
using TFraese;
using UnityEngine;

/// <summary>
/// Abstract class for representing multiverse-time coordinates and board
/// coordinates. mvtt is stored in hyperVector, board in subVector
/// </summary>
public class HyperVector
{
	// coordinate in MVTime. Can be null
	public NVector hyperVector;

	// coordinate in local board space.
	public NVector subVector;

	// TODO: Refactor rider sets so that they do this in place and also
	// clean this up.
	public HyperVector Add(HyperVector v)
	{
		return new HyperVector(
				Combinatorics.Add(this.hyperVector.array, v.hyperVector.array),
				Combinatorics.Add(this.subVector.array, v.subVector.array)
			);
	}
	// Constructor combinations for convenience
	public HyperVector(int[] subArray)
	{
		this.hyperVector = null;
		this.subVector = new NVector(subArray);
	}
	public HyperVector(int[] hyperArray, int[] subArray)
	{
		this.hyperVector = new NVector(hyperArray);
		this.subVector = new NVector(subArray);
	}
	public HyperVector(NVector subArray)
	{
		this.hyperVector = null;
		this.subVector = subArray;
	}
	public HyperVector(NVector hyperVector, NVector subVector)
	{
		this.hyperVector = hyperVector;
		this.subVector = subVector;
	}
	public HyperVector(HyperVector hyperVector)
	{
		this.hyperVector = new NVector(hyperVector.hyperVector.array);
		this.subVector = new NVector(hyperVector.subVector.array);
	}
	public static HyperVector operator +(HyperVector v1, HyperVector v2)
	{
		// Hypervectors should never be added with null hypervectors, so just let
		// the null reference exception flag here if its an issue.
		return new HyperVector(
				v1.hyperVector + v2.hyperVector,
				v1.subVector + v2.subVector
			);
	}
}
