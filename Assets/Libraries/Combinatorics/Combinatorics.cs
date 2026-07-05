using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TFraese
{
    /*
     * Some major combinatorial methods included will be generation of binary
     * sequences of lenght n with c 1's (for rider movement and sign generation)
     * the composition of sets, to apply sign generation to rider movement,
     * permutation methods in regards to chess piece permuatable movements,
     * and eventually board groupings into sets of either 1 or 2 to enumerate
     * multiversal options between boards.
     */

    /// <summary>
    /// Handles Combinatorial methods, primarily used in piece generation
    /// </summary>
    public static class Combinatorics
    {
		// Pre-calculated Pascal's Triangle lookup table for n = 0 to 16
		// Since n in most cases directly corresponds to the number of dimensions
		// we're generating in, 16 is plenty. anything about 7 or 8 is humanly
		// unplayable anyway.
		private static readonly int[][] PascalTable = new int[][]
		{
			new int[] {1},
			new int[] {1, 1},
			new int[] {1, 2, 1},
			new int[] {1, 3, 3, 1},
			new int[] {1, 4, 6, 4, 1},
			new int[] {1, 5, 10, 10, 5, 1},
			new int[] {1, 6, 15, 20, 15, 6, 1},
			new int[] {1, 7, 21, 35, 35, 21, 7, 1},
			new int[] {1, 8, 28, 56, 70, 56, 28, 8, 1},
			new int[] {1, 9, 36, 84, 126, 126, 84, 36, 9, 1},
			new int[] {1, 10, 45, 120, 210, 252, 210, 120, 45, 10, 1},
			new int[] {1, 11, 55, 165, 330, 462, 462, 330, 165, 55, 11, 1},
			new int[] {1, 12, 66, 220, 495, 792, 924, 792, 495, 220, 66, 12, 1},
			new int[] {1, 13, 78, 286, 715, 1287, 1716, 1716, 1287, 715, 286, 78, 13, 1},
			new int[] {1, 14, 91, 364, 1001, 2002, 3003, 3432, 3003, 2002, 1001, 364, 91, 14, 1},
			new int[] {1, 15, 105, 455, 1365, 3003, 5005, 6435, 6435, 5005, 3003, 1365, 455, 105, 15, 1},
			new int[] {1, 16, 120, 560, 1820, 4368, 8008, 11440, 12870, 11440, 8008, 4368, 1820, 560, 120, 16, 1}
		};

		// C(n,k) by sampling from pascals triangle
		public static int Choose(int n, int k)
		{
			// Out of bounds guards
			if (n < 0 || n > 15 || k < 0 || k > n)
			{
				Debug.LogWarning("Calling C(n,k) out of precalculated bounds");
				return 0;
			}
			return PascalTable[n][k];
		}

		// Method courtesy of Gemini. If it ends up not working just replace it
		// with previous nD chess method.
		public static int[][] BitString(int n, int c)
		{
			if (c < 0 || c > n || n < 1 || n > 15) // Limited to 15 bits for precalc
			{
				Debug.LogWarning("Calling BitString(n,k) out of precalculated bounds");
				return Array.Empty<int[]>();
			}
			if (c == 0)
			{
				int[][] result = new int[1][];
				result[0] = new int[n];
				return result;
			}

			// Pre-calculate exact number of combinations (Binomial Coefficient)
			int totalCombinations = Choose(n, c);
			int[][] results = new int[totalCombinations][];

			// Start with the lowest lexicographical value with c ones (e.g., c=3 -> 000...0111)
			int mask = (1 << c) - 1;
			int limit = 1 << n;
			int count = 0;

			while (mask < limit && count < totalCombinations)
			{
				// 1. Convert the integer bits directly into the int[n] array
				int[] current = new int[n];
				for (int i = 0; i < n; i++)
				{
					current[n - 1 - i] = (mask >> i) & 1;
				}
				results[count] = current;
				count++;

				// 2. Gosper's Hack: Advance to the next unique bit permutation
				// TODO: understand this better
				int c_bit = mask & -mask;
				int r = mask + c_bit;
				mask = (((r ^ mask) >> 2) / c_bit) | r;
			}
			return results;
		}
		// Method somewhat courtesy of gemini. Manually converted to precalc-
		// ulate array size and use int[][] rather than list<int[]>.
		/// <summary>
		/// Generates all unique combinations of k integers ranging from 0 to n-1.
		/// Elements within each combination are strictly increasing.
		/// </summary>
		public static int[][] CombinationsUniqueSequential(int n, int k)
		{
			int length = Choose(n, k);
			int count = 0;
			int[][] result = new int[length][];


			// Edge case checks
			if (k < 0 || k > n || n > 15)
			{
				Debug.LogWarning("Calling ComboUniqueSequential(n,k) out of precalculated bounds");
				return Array.Empty<int[]>();
			}

			// Initialize x with the first k bits set to 1 (e.g., k=3 -> 00000111)
			int x = (1 << k) - 1;
			int limit = 1 << n;

			while (x < limit)
			{
				int[] currentCombination = new int[k];
				int index = 0;

				// Map the set bits of 'x' to the numbers 0 through n-1
				for (int i = 0; i < n; i++)
				{
					if ((x & (1 << i)) != 0)
					{
						currentCombination[index++] = i;
					}
				}
				result[count] = currentCombination;
				count++;

				// Gosper's Hack to find the next lexicographical integer with exactly k set bits
				int lowest = x & -x;
				int next = x + lowest;

				// Shift operations handle the trailing bits efficiently
				x = (((x ^ next) >> 2) / lowest) | next;
			}

			return result;
		}

	}
}
