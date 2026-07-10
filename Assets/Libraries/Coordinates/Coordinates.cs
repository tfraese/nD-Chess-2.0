using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TFraese
{
	/// <summary>
	/// Handles basic index <-> integer array conversion, mainly for accessing
	/// flattened 1D array representing n-D board states.
	/// </summary>
	public class Coordinates
	{
		/// <summary>
		/// Generate a coordinate in an n-dimensional hypercuboid from an index.
		/// Least significant modulo of index sorted left. 1 -> (1,0,0,0)
		/// </summary>
		public static int[] IndexToCoordinate(int index, int[] dimensions)
		{
			int rank = dimensions.Length;
			int[] coordinates = new int[rank];
			for (int i = rank - 1; i >= 0; i--)
			{
				coordinates[dimensions.Length - i - 1] = index % dimensions[i];
				index /= dimensions[i];
			}
			return coordinates;
		}
		/// <summary>
		/// Generate an index from a coordinate in an n-dimensional hypercuboid
		/// Least significant modulo of index sorted left. (1,0,0,0) -> 1
		/// </summary>
		public static int CoordinateToIndex(int[] coordinates, int[] dimensions)
		{
			int rank = dimensions.Length;
			int index = 0;
			int multiplier = 1;
			for (int i = rank - 1; i >= 0; i--)
			{
				index += coordinates[dimensions.Length - i - 1] * multiplier;
				multiplier *= dimensions[i];
			}
			return index;
		}
		// Find the volume (number of cells) in a hyper-cuboid of a given dimensions
		public static int Volume(int[] dimensions)
		{
			int result = 1;
			for(int i = 0; i < dimensions.Length; i++)
			{
				result *= dimensions[i];
			}
			return result;
		}
	}
}
