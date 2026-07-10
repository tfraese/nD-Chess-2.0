using System.Collections;
using System.Collections.Generic;
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
}
