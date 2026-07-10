using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Standard n-D hyper-cuboid board
/// </summary>
public class BoardCartesian : Board
{
    public BoardCartesian(BoardLayout layout)
    {
        base.state = new BoardState(layout.startingState);
        int[] dimensions = layout.dimensions;
		this.boardName = "Cartesian-" + DString(dimensions);
	}
    /// <summary>
    /// Generates a blank board of specified dimension. Should not be used. If
    /// a fresh blank board is needed, supply a layout with a blank state.
    /// </summary>
    public BoardCartesian(int[] dimensions)
    {
        base.state = new BoardState(dimensions);
        this.boardName = "Cartesian-" + DString(dimensions);
    }
    /// <summary>
    /// Generate a string based on the dimensionality and bounds of the board.
    /// </summary>
    public string DString(int[] dimensions)
    {
        string result = "";
        for(int i = 0; i < dimensions.Length; i++)
        {
            string term = i == dimensions.Length - 1 ? "x" : "";
            result += dimensions[i].ToString() + term;
        }
        return result;
    }
}
