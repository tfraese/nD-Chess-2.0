using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Standard n-D hyper-cuboid board
/// </summary>
public class Cartesian : BoardType
{

    public Cartesian(string boardName, int[] dimensions)
    {
        base.boardName = boardName;
        base.dimensions = (int[])dimensions.Clone();
    }
}
