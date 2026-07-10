using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Evaluate if this is the best place to store this
/// <summary>
/// Classifications for axes. Internally Forward, Lateral, and None should be
/// the only options used. Multiverse and Time will be used to annotate
/// the hyperVector of the HyperVector class.
/// </summary>
public enum Axes
{
	Forward,
	Lateral,
	None,
	Multiverse,
	Time
}

/// <summary>
/// Abstract class for defining a board layout. The Board Class holds the
/// dimensionality of itself internally, but this should be the way to
/// access that information.
/// </summary>
public class BoardLayout
{
	public int[] dimensions;
	public Axes[] axesTypes;
	public BoardState startingState;

	/// <summary>
	/// Return an array of axes for move generation, specifically pawns and shogi
	/// pieces.
	/// </summary>
	public int[] GetAxes(Axes axesType)
	{
		int[] result = new int[dimensions.Length];
		for (int i = 0; i < dimensions.Length; i++)
		{
			if (axesTypes[i] == axesType)
			{
				result[i] = 1;
			}
		}
		return result;
	}
}
