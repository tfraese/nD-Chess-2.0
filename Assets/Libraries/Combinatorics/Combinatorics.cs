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
    /// Handles Combinatorial methods for piece generation. Limited to small
	/// numbers of n for now.
    /// </summary>
    public static class Combinatorics
    {
		// Pre-calculated Pascal's Triangle lookup table for n = 0 to 16
		// Since n in most cases directly corresponds to the number of dimensions
		// we're generating in, 16 is plenty. anything about 7 or 8 is humanly
		// unplayable anyway.

		// TODO: Precalculate this up to larger n for the engines mvtime AI.
		// Will likely need more specialized Combinatorics library / tools for
		// multiverse time travel timeline evaluations.
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
		// Precalculated factorials for 0 <= n <= 12
		// 12! is the highest factorial that fits inside a standard 32-bit signed int
		private static readonly int[] Factorials = new int[]
		{
			1,          // 0!
			1,          // 1!
			2,          // 2!
			6,          // 3!
			24,         // 4!
			120,        // 5!
			720,        // 6!
			5040,       // 7!
			40320,      // 8!
			362880,     // 9!
			3628800,    // 10!
			39916800,   // 11!
			479001600   // 12!
		};

		public static int Factorial(int n)
		{
			if (n < 0 || n >= Factorials.Length)
			{
				Debug.LogWarning("Calling Factorial() out of precalculated bounds");
				return 0;
			}
			return Factorials[n];
		}

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

		// Method courtesy of Bill Gosper, American mathematician, and gemini
		// If it ends up not working just replace it with previous nD chess method.
		/// <summary>
		/// Generate All bitstrings of length n with a count of c 1-bits
		/// </summary>
		public static int[][] Bitstrings(int n, int c)
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
		/// <summary>
		/// Generate all bitstrings of length n
		/// </summary>
		public static int[][] Bitstrings(int n)
		{

			int count = 1 << n;
			int[][] result = new int[count][];
			for (int i = 0; i < count; i++)
			{
				result[i] = new int[n];
				for (int b = 0; b < n; b++)
				{
					result[i][b] = (i >> b) & 1;
				}
			}
			return result;
		}

		/// <summary>
		/// Generates all combinations of c elements from the input array. Input array
		/// is assumed to contain only unique items
		/// </summary>
		public static int[][] Combinations(int[] uniqueItems, int c)
		{
			int n = uniqueItems.Length;
			// Edge case checks
			if (c < 0 || c > n || n > 15)
			{
				Debug.LogWarning("Calling ComboUniqueSequential(n,k) out of precalculated bounds");
				return Array.Empty<int[]>();
			}

			int[][] sequential = CombinationsUniqueSequential(n, c);
			int[][] result = new int[Choose(n, c)][];

			for (int seq = 0; seq < sequential.Length; seq++)
			{
				result[seq] = new int[c];
				for (int i = 0; i < c; i++)
				{
					int[] sequence = sequential[seq];
					int itemIndex = sequence[i];
					result[seq][i] = uniqueItems[itemIndex];
				}
			}
			return result;
		}

		// Courtesy of Narayana Pandita, 14th century Indian Mathematician,
		// wrapped by gemini
		/// <summary>
		/// Generates all permutations of the firts n non-negative integers.
		/// </summary>
		public static int[][] PermuteSequential(int n)
		{
			if (n <= 0)
			{
				return new int[][] { Array.Empty<int>() };
			}
			if (n == 1)
			{
				return new int[][] { new int[1] };
			}

			// Initialize the base array: [0, 1, 2, ..., n-1]
			int[] current = new int[n];
			for (int i = 0; i < n; i++)
			{
				current[i] = i;
			}

			var result = new List<int[]>();

			// Add the first initial permutation
			int[] firstCopy = new int[n];
			Array.Copy(current, firstCopy, n);
			result.Add(firstCopy);

			// Narayana Pandita's lexicographical permutation algorithm
			while (true)
			{
				// 1. Find the largest index i such that current[i] < current[i + 1]
				int i = n - 2;
				while (i >= 0 && current[i] >= current[i + 1])
				{
					i--;
				}

				// If no such index exists, we have reached the last permutation
				if (i < 0)
				{
					break;
				}

				// 2. Find the largest index j greater than i such that current[i] < current[j]
				int j = n - 1;
				while (current[j] <= current[i])
				{
					j--;
				}

				// 3. Swap the values at indices i and j
				int temp = current[i];
				current[i] = current[j];
				current[j] = temp;

				// 4. Reverse the sequence from index i + 1 up to the end
				int left = i + 1;
				int right = n - 1;
				while (left < right)
				{
					temp = current[left];
					current[left] = current[right];
					current[right] = temp;
					left++;
					right--;
				}

				// Store the newly generated permutation
				int[] permCopy = new int[n];
				Array.Copy(current, permCopy, n);
				result.Add(permCopy);
			}

			return result.ToArray();
		}

		/// <summary>
		/// Generates all permutations of items supplied. Assumes items are
		/// unique.
		/// </summary>
		public static int[][] Permutations(int[] uniqueItems)
		{
			int n = uniqueItems.Length;
			// Edge case checks
			if (n > 12)
			{
				Debug.LogWarning("Calling ComboUniqueSequential(n,k) out of precalculated bounds");
				return Array.Empty<int[]>();
			}

			int[][] sequential = PermuteSequential(n);
			int[][] result = new int[Factorial(n)][];

			for (int seq = 0; seq < sequential.Length; seq++)
			{
				result[seq] = new int[n];
				for (int i = 0; i < n; i++)
				{
					int[] sequence = sequential[seq];
					int itemIndex = sequence[i];
					result[seq][i] = uniqueItems[itemIndex];
				}
			}
			return result;
		}

		/// <summary>
		/// Given an array of unique items, and an associate frequency array,
		/// generate all unique permutations of items occuring at that frequency.
		/// </summary>
		public static int[][] Permutations(int[] items, int[] frequency)
		{
			if (items.Length != frequency.Length)
			{
				Debug.LogError("Items and Frequency length do not match in Permutations()");
				return new int[][] { Array.Empty<int>() };
			}
			int itemCount = 0;
			for (int i = 0; i < items.Length; i++)
			{
				itemCount += frequency[i];
			}
			if (itemCount > 15)
			{
				Debug.LogError("Resulting sequence would go outside precalculated Choose bounds");
				return new int[][] { Array.Empty<int>() };
			}

			// Start with a single sequence of the first item i0 repeated f0 times
			int[][] result = new int[1][];
			int f0 = frequency[0];
			result[0] = new int[f0];
			for (int i = 0; i < f0; i++)
			{
				result[0][i] = items[0];
			}

			for (int index = 1; index < items.Length; index++)
			{
				// Given the next item in the array occuring a frequency fi, we will sort
				// those fi identical items into the spaces in between elements of the
				// current base sequence. i.e. this is a stars and bars problem.
				int item = items[index];

				// get the frequency the new item must occur at fi
				int fi = frequency[index];

				if (fi == 0) continue;

				// get the number of spaces between base sequence elements we sort the
				// new item occurances into. Note all elements in the result array
				// should have equal length, so we only have to check th 0th element
				int k = result[0].Length + 1;

				// calculate the length of the new array (the sum of fi and the base
				// sequences array)
				int n = fi + k - 1;

				// Generate combinations (in strictly increasing order) of which bins
				// or spots between the base sequence elements in which to sort the
				// new items
				int[][] seq = CombinationsUniqueSequential(n, fi);

				// generate a temp result array that we will swap with the final one
				// as we go along.
				int[][] temp = new int[result.Length * seq.Length][];
				int tempCount = 0;

				for (int baseSeq = 0; baseSeq < result.Length; baseSeq++)
				{
					int[] baseSequence = result[baseSeq];
					//Debug.Log($"Base Sequence: {Arrays.ToString(baseSequence)}");
					for (int injSeq = 0; injSeq < seq.Length; injSeq++)
					{


						// output array element to place into temp int[][]
						int[] injected = new int[n];
						
						
						// combinations of sequential integers {s | 0 <= s < n}
						// ex. C(3, 2) -> {0,1} {0,2} {1,2}. Note they are
						// strictly increasing.
						int[] injectionSequence = seq[injSeq];
						temp[tempCount] = Inject(item, baseSequence, injectionSequence);

						tempCount++;
					}

				}
				// TODO: Investigate if this needs to evade garbage collector,
				// just convert it to a list<int[]> and cast to array on return
				// if so.
				result = temp;
			}

			// now the terrifying part, to test this...
			return result;
		}

		/// <summary>
		/// Generate all possible signed sequences from a set of base sequences
		/// and a set of bitstrings. Applies all bitstrings to each sequence
		/// </summary>
		public static int[][] Sign(int[][] sequences, int[][] bitstrings)
		{
			int[][] result = new int[sequences.Length * bitstrings.Length][];
			int count = 0;

			for (int s = 0; s < sequences.Length; s++)
			{
				for (int b = 0; b < bitstrings.Length; b++)
				{
					result[count] = Sign(sequences[s], bitstrings[b]);
					count++;
				}
			}
			return result;
		}

		/// <summary>
		/// Sign a sequence based on the given bitstring. 0 results in positive,
		/// 1 in negative values. ex. (1,2,3) * (1,0,0) = (-1,2,3)
		/// </summary>
		public static int[] Sign(int[] sequence, int[] bitstring, bool performSafetyChecks)
		{
			if (!performSafetyChecks)
			{
				return Sign(sequence, bitstring);
			}
			if (sequence.Length != bitstring.Length)
			{
				Debug.LogError("Sequence and bitstring length mismatch");
				return null;
			}
			return Sign(sequence, bitstring);
		}

		/// <summary>
		/// Sign a sequence based on the given bitstring. 0 results in positive,
		/// 1 in negative values. ex. (1,2,3) * (1,0,0) = (-1,2,3)
		/// </summary>
		public static int[] Sign(int[] sequence, int[] bitstring)
		{
			int[] result = new int[sequence.Length];
			for (int i = 0; i < sequence.Length; i++)
			{
				result[i] = bitstring[i] == 0 ? sequence[i] : -sequence[i];
			}
			return result;
		}

		// Based on the alorithm and method above
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

		/// <summary>
		/// Injects an item using every supplied injection sequence into every
		/// supplied base sequence. Injection sequences are assumed to be
		/// sequential lists of length n of non-negatives less than n
		/// </summary>
		public static int[][] Inject(int item, int[][] baseSequences, int[][] injectionSequences)
		{
			int[][] result = new int[baseSequences.Length*injectionSequences.Length][];
			int count = 0;
			for (int s = 0; s < baseSequences.Length; s++)
			{
				for (int i = 0; i < injectionSequences.Length; i++)
				{
					result[count] = Inject(item, baseSequences[s], injectionSequences[i]);
					count++;
				}
			}
			return result;
		}

		/// <summary>
		/// Inject specified item into the base sequence. Injection sequence
		/// refers to slots before, between and after base sequence elements.
		/// Performs safety checks
		/// </summary>
		public static int[] Inject(int item, int[] baseSequence, int[] injectionSequence, bool performSafetyChecks)
		{
			if (!performSafetyChecks)
			{
				return Inject(item, baseSequence, injectionSequence);
			}

			Debug.LogWarning("Safety checks for Inject() not imeplemented yet");	
			return Inject(item, baseSequence, injectionSequence);
		}

		/// <summary>
		/// Inject specified item into the base sequence. Injection sequence
		/// refers to slots before, between and after base sequence elements
		/// and must be non-negative sequential integers less than the base
		/// sequence's length n
		/// </summary>
		/// <param name="injectionSequence"></param>
		/// <returns></returns>
		public static int[] Inject(int item, int[] baseSequence, int[] injectionSequence)
		{
			int n = baseSequence.Length + injectionSequence.Length;
			int[] injected = new int[n];

			int j = 0; // index of the resulting sequence
			int s = 0; // index of the base sequence
			int i = 0; // index of the injection sequence

			// TODO: abstract the injection process into its own
			// sub method.

			// Inject (int[] sequence, int item, int[] positions)

			// Until we are finished writing the output sequence
			while (j < n)
			{
				// check to see if this is an index we inject the
				// new item into.
				if (i < injectionSequence.Length && j == injectionSequence[i])
				{
					// if so, write the new item to the sequence
					injected[j] = item;

					// move on to the next target. Note that because
					// we generate the combination sequences to be
					// strictly increasing, we won't miss any spots.
					i++;
					j++;
				}
				else
				{
					// otherwise, write the next element in the base
					// sequence that we are injecting new items into
					injected[j] = baseSequence[s];
					s++;
					j++;
				}
			}
			return injected;
		}
	}
}
