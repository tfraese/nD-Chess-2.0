using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract wrapper object for arrays. Coordinate of arbitrary dimension
/// </summary>
public class NVector
{
    public int[] array;
    public NVector(int[] array)
    {
        this.array = (int[])array.Clone();
    }
}
