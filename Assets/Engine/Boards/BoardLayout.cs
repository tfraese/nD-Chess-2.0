using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Axes
{
	Forward,
	Lateral,
	None,
	Multiverse,
	Time
}

/// <summary>
/// Abstract class for defining a board layout.
/// </summary>
public class BoardLayout
{
	public int[] bounds;
	public Axes[] axesTypes;

	public int[] GetAxes(Axes axesType)
	{
		int[] result = new int[bounds.Length];
		for (int i = 0; i < bounds.Length; i++)
		{
			if (axesTypes[i] == axesType)
			{
				result[i] = 1;
			}
		}
		return result;
	}
}
