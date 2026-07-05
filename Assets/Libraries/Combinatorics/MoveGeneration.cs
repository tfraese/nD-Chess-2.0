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
        /* 
         * Generate a sequence of numbers. Each number in the sequence re-
         * presents a dimension the rider is choosing to move along in that
         * specific sequence. {1,3} (0 indexed) is {y, w}. To avoid choosing the
         * same dimension twice, the number is unique. To avoid repeated combi-
         * nations, the numbers are sequential.
         */
        int[][] sequentialCombinations = Combinatorics.CombinationsUniqueSequential(n, c);

        /*
         * Next we are generating a sequence of bitstrings of length c, in
         * with batches of a count of 1's from 0 to c. These represent the sign
         * of the 1st, 2nd,...,cth (1-indexed) 1 in the binary string.
         * 
         * Since this is equivalent to taking C(c,k) from 0 to k (i.e. all
         * entries in pascal's triangle) the total number of options is the sum
         * across the cth row of pascal's triangle: 2^c.
         * 
         * We could technically just count them up from 0 to (2^c - 1) in binary
         * but this method organizes them, and its already implemented.
         */
        int bitstringLength = (1 << c);
        int bitstringCount = 0;
        int[][] bitstrings = new int[bitstringLength][];
        for (int onesCount = 0; onesCount <= c; onesCount++)
        {
            int[][] bitstringsLocal = Combinatorics.BitString(c, onesCount);
            for (int i = 0; i < bitstringsLocal.Length; i++)
            {
                bitstrings[bitstringCount] = (int[])bitstringsLocal[i].Clone();
                bitstringCount++;
            }
        }

        /*
         * Here we take a given sequence. Take the sequence {1,5} for example,
         * with the bit string {0,1}
         * 
         * We take the 0th element in the sequence (1), and the 0th bit in the
         * binary string. We mark the 1st (0-indexed) component of the output
         * vector as positive because the bit is 0, and mark it as +1 because
         * of the sequence element.
         * 
         * Next, we take the 1th sequence element (5) and the 2th bit (1). We
         * mark this component negative because of the bit 1, and mark it -1
         * because of the sequence element.
         * 
         * {1, 5} x {0, 1} = {0, 1, 0, 0, 0, -1, 0, 0, 0, 0...}
         */
        int directionLength = Combinatorics.Choose(n, c) * (1 << c);
        int directionCount = 0;

        int[][] directions = new int[directionLength][];
        foreach (var sequence in sequentialCombinations)
        {
            foreach (var bitString in bitstrings)
            {
                directions[directionCount] = new int[n];
                for (int i = 0; i < sequence.Length; i++)
                {
                    int sign = bitString[i] == 0 ? 1 : -1;
                    directions[directionCount][sequence[i]] = sign * 1;
                }
                directionCount++;
            }
        }
        /*
         * By taking all elements of the Sequences S and Bitstrings B we recieve
         * S x B which is all directions a rider can move in the specified
         * dimensions.
         */
        return directions;
    }
    
}
