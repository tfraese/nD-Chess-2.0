using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TFraese;
using UnityEngine;

// TODO: Cut down documentation and comments now that problem is solved.

/*
 * Convolutions are transformations (matrix multiplications really) that will
 * augment color dependent movesets based on which color is making the move.
 * 
 * Consider a 4 player 4D game where each player has two forwards. In this
 * case specifically, we generally don't want any players to share forward
 * any forward directions. This means our options are fairly simple. Since
 * forward and laterals are indistinguisable, we can find any combination
 * of swapping foward and lateral pairs, and perform the swap.
 * 
 * (x,y)(z,w) is indistinguishable from (x,w)(z,y), though each one will
 * result in a different exact vector order, the total set will be the same.
 * 
 * Additionally, we want to either flip all forward directions, or flip none.
 * this gives us 4 options, with one representation set being
 * { +(x)(y)(z)(w) : -(x)(y)(z)(w) : +(x,y)(z,w) : -(x,y)(z,w) }
 * 
 * Side note, this problem of grouping all possible elements into pairs or
 * alone is very similar to generating moves between active boards in mvtime.
 * 
 * An 8 player 4D game neccesitates that 3 lateral directions exist. one way
 * to represent this is (with a forward y direction)
 * { +(x)(y)(z)(w), +(x,y)(z)(w), +(x)(y,z)(w), +(x)(z)(y,w),
 *   -(x)(y)(z)(w), -(x,y)(z)(w), -(x)(y,z)(w), -(x)(z)(y,w) }
 * 
 * One thing to note is that to satisfy the condition that no players share
 * forward directions the number of possible players for a dimensionality
 * is dependent on the number of forward directions, with some combinations
 * having no possibilities whatsoever. Dropping this constraint could still
 * lead to some interesting possibilities though, so it may be worth pursuing
 * a mathematical framework that allows for that option as well.
 * 
 * Multiverse time travel rules throws a further complication into this mix.
 * TODO: Figure that out once mvtime pawn and brawn definitions are consistent
 * with communities'
 * 
 * Generation will likely go as follows. Consider an n-D board with c players
 * and f forward axes, meaning we get 2f forward directionals. For players to
 * not share any forward directions, that means we 
 */
public class Convolution
{
	List<int[]> swaps;
	/// <summary>
	/// Should be constructed from a list of int[2]'s which hold what indices of
	/// a given array should be swapped. (x,y,z,w) -> (y,x,w,z) would
	/// be represented with { [0,1], [2,3] }. Unsafe construction
	/// </summary>
	private Convolution(List<int[]> swaps=null)
	{
		this.swaps = swaps;
        if (swaps == null)
        {
			this.swaps = new List<int[]>();
        }
	}

	/// <summary>
	/// swap and flip the orientation of a vector. Forward vectors are neccesary
	/// to generate a mask for flipping. Unsafe execution.
	/// </summary>
	public int[] Convolute(int[] array, Axes[] axesTypes, bool flip)
	{
		int[] result = (int[])array.Clone();
		// TODO: Consider factoring out flip to be a seperate Combinatorics.cs
		// method so that axesTypes dont need to be referenced.
		if (flip)
		{
			for (int i = 0; i < result.Length; i++)
			{
				if (axesTypes[i] == Axes.Forward)
				{
					result[i]= -result[i];
				}
			}
		}
		// perform the swaps.
		for (int i = 0; i < swaps.Count; i++)
		{
			int temp = result[swaps[i][0]];
			result[swaps[i][0]] = result[swaps[i][1]];
			result[swaps[i][1]] = temp;
		}
		return result;
	}

	/*
	 * Convolution generation under the restriction that no player shares any
	 * forward direction goes as follows. We enumerate all possibilities of
	 * forward directions, and then go through and pick matching laterals.
	 * We discard the already used laterals, and then we pick the next f laterals
	 * for the same set of forwards. We generate these in stages, first determining
	 * the index of the forward and lateral within their enumerated lists, and then
	 * using those enumerated lists to substitute out the actual values.
	 * 
	 * ex. Consider convoluting (l, f, l, f, l, l, l, l)
	 * F = {1, 3} L = {0, 2, 4, 5, 6, 7}
	 * 
	 * pass 1:
	 * [0, 0] [1, 1]
	 * [0, 2] [1, 3]
	 * [0, 4] [1, 5]
	 * 
	 * note that pass 1 could be done with a simple for loop once we determine the
	 * number of forward directions. i.e.
	 * 
	 * for (i < n - f) [i % f] [i]
	 *		
	 * pass 2: sub
	 * []
	 * [1, 0] [3, 2]
	 * [1, 4] [3, 5]
	 * [1, 6] [3, 7]
	 * 
	 * resulting in list of convolution.
	 */

	// TODO: perform this with integer arithmetic instead.
	public static List<Convolution> Generate(Axes[] axesTypes)
	{
		// Enumerate the forward and lateral axes into their own lists
		List<int> forwardIndices = new List<int>();
		List<int> lateralIndices = new List<int>();
		for (int i = 0; i < axesTypes.Length; i++)
		{
			if (axesTypes[i] == Axes.Forward)
			{
				forwardIndices.Add(i);
			}
			if (axesTypes[i] == Axes.Lateral)
			{
				lateralIndices.Add(i);
			}
		}

		int forwardCount = forwardIndices.Count;
		int lateralCount = lateralIndices.Count;
		int convolutionCount = 1 + lateralCount / forwardCount;

		List<Convolution> convolutions = new List<Convolution>();
		// Add an empty convolution to represent leaving the array untouched
		convolutions.Add(new Convolution());
		
		// TODO: Factor this into integer and modulo arithmetic.
		int currentLateral = 0;
		for (int currentPlayer = 1; currentPlayer < convolutionCount; currentPlayer++)
		{
			List<int[]> swaps = new List<int[]>();
			for (int forward = 0; forward < forwardCount; forward++)
			{
				int[] swap = new int[2];
				swap[0] = forward;

				swap[1] = currentLateral;
				currentLateral++;

				swaps.Add(swap);
			}
			convolutions.Add(new Convolution(swaps));
		}

		// Perform a second pass where we substitue out the original convolution for
		// one comprising the indices of where the forward and lateral directions are
		// in their original array, instead of their enumerated lists.
		List<Convolution> convolutionsShifted = new List<Convolution>();
		for (int conv = 0; conv < convolutions.Count; conv++)
		{
			List<int[]> swaps = convolutions[conv].swaps;
			List<int[]> swapsShifted = new List<int[]>();
			for (int sw = 0; sw < swaps.Count; sw++)
			{
				int[] currentSwap = swaps[sw];
				int[] shifted = new int[2];
				shifted[0] = forwardIndices[currentSwap[0]];
				shifted[1] = lateralIndices[currentSwap[1]];
				swapsShifted.Add(shifted);
			}
			convolutionsShifted.Add(new Convolution(swapsShifted));
		}

		return convolutionsShifted;
	}

	public override string ToString()
	{
		string result = "{\n";
		for (int i = 0; i < swaps.Count; i++)
		{
			string term = (i == swaps.Count - 1) ? "" : ",\n";
			result += Arrays.ToString(swaps[i]) + term;
		}
		result += "\n}";
		return result;
	}
}
