using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TFraese;

/// <summary>
/// Abstract wrapper object for arrays. Coordinate of arbitrary dimension
/// </summary>
public class NVector
{
    public int[] array;
    public NVector(int[] array)
    {
        this.array = (int[])array?.Clone();
    }
	public override string ToString()
	{
		return "\n" + Arrays.ToString(array);
	}
    public static NVector operator +(NVector v1, NVector v2)
    {
        // We unfortunately need null checks here if we are going to add
        // hypervectors together.

        // TODO: Investigate if performance gain is significant and necessary
        // Would have to override singularity functions with NVectors.
        if (v1 == null && v2 == null) return null;
        if (v1 == null) return v2;
        if (v2 == null) return v1;
        return new NVector(Combinatorics.Add(v1.array, v2.array));
    }
}
