using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TFraese;
using System.Linq;

public static class MoveGeneration
{
	/// <summary>
	/// Generates an array of all directions that a rider of order c agonal can
	/// travel in n dimensions. Outputs an int[][] with interior array elements
	/// being -1, 0, or 1, representing the vector of travel of a piece
	/// </summary>
	/// <param name="n">Number of dimensions</param>
	/// <param name="c">c-agonal of rider piece</param>
	/// <returns></returns>
    public static int[][] RiderDirections(int n, int c)
    {
        if (c > n)
        {
            Debug.LogWarning($"Generating {c} rider for {n} < {c}, empty moveset created");
            return new int[0][];
        }

        // generate a list of c 1's
        int[] ones = new int[c];
        for (int i = 0; i < c; i++)
        {
            ones[i] = 1;
        }
        int[][] onesWrapper = new int[][] { ones };

        // generate all bitstrings of length c
        int[][] bitstrings = Combinatorics.Bitstrings(c);

		// sign the list of c 1's
		int[][] signed = Combinatorics.Sign(onesWrapper, bitstrings);

		// calculate number of zeros to inject into signed sequences
		int zeroCount = n - c;

        // if we dont need to inject any zeros just return the signed values
        if (zeroCount == 0)
        {
            return signed;
        }

        // calculate all combinations of ways to pad zero into the spaces before
        // after and between signed one's
        int[][] injectionSequences = Combinatorics.CombinationsUniqueSequential(n, zeroCount);

        // inject 0's into the sequence in all possible permutations to pad the
        // vectors into n dimensions.
		return Combinatorics.Inject(0, signed, injectionSequences);
    }

    /// <summary>
    /// Generates all move vectors for a piece that can move any permutation of
    /// provided offsets in any directions. Assumes components are unique.
    /// </summary>
    public static int[][] Permutables(int n, int[] components)
    {
        int[] freq = new int[components.Length];
        for (int i = 0; i < freq.Length; i++)
        {
            freq[i] = 1;
        }
        return Permutables(n, components, freq);
    }

    /// <summary>
    /// Generates all move vectors for a piece that can move any permutation of
    /// provided offsets in any directions. Assumes components are unique. Use
    /// a frequency array for repeat elements.
    /// </summary>
    public static int[][] Permutables(int n, int[] components, int[] freq)
    {
        // calculate the number of non-zero components based on the frequency
        int c = 0;
        for (int i = 0; i < freq.Length; i++)
        {
            c += freq[c];
        }

        // warn the console if we create an empty moveset
        if (c > n)
        {
            Debug.LogWarning($"Generating {c} permutable for {n} < {c}, empty moveset created");
            return new int[0][];
        }

        // permute the values given
        int[][] permutations = Combinatorics.Permutations(components, freq);

        // generate bitstrings to sign them
        int[][] bitstrings = Combinatorics.Bitstrings(c);

        // sign the permutations
        int[][] signed = Combinatorics.Sign(permutations, bitstrings);

        // calculate zero count to fill
        int zeroCount = n - c;
        if (zeroCount == 0) { return signed; }

        // generate injection sequences
        int[][] injectionSequences = Combinatorics.CombinationsUniqueSequential(n, zeroCount);

        // inject the number of zeros needed to pad out to n dimensions
        int[][] injected = Combinatorics.Inject(0, signed, injectionSequences);
        return injected;
    }
	/// <summary>
	/// Generates a moveset dependent on color of the player and direction,
	/// that can move f squares forward. Dependent on board layout
	/// </summary>
	public static int[][] Directionals(int f, int[] forwards)
    {
        return Directionals(f, forwards, 0, null);
    }
    /// <summary>
    /// Generates a moveset dependent on color of the player and direction,
    /// that can move f squares forward and l squares sideways. Dependent
    /// on board layout
    /// </summary>
    public static int[][] Directionals(int f, int[] forwards, int l, int[] laterals)
    {
        // Split the supplied lateral offsets into multiples of basis vectors
        int[][] forwardSet = Combinatorics.Split(forwards);
        int[][] forwardOffsets = Combinatorics.Multiply(forwardSet, f);

        // if no lateral offset was defined, just return the forward options
        if (laterals == null || laterals.Length == 0)
        {
            return forwardOffsets;
        }

        int[][] lateralSet = Combinatorics.Split(laterals);
        int[][] lateralOffsets = Combinatorics.Multiply(lateralSet, l);


        // generate a set of scalars for original, and mirrored lateral offsets
        int[] scalars = new int[] {1, -1};

        // generate list of original and mirrored lateral vectors
        int[][] lateralsSigned = Combinatorics.Multiply(lateralSet, scalars);
        
        // Recombine each of the forward and signed lateral vectors into the
        // final moveset.
        int[][] result = Combinatorics.Add(forwardSet, lateralsSigned);
        return result;
    }
}
