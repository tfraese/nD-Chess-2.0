using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Multiverse representing a traditional, non-time travel game.
/// Abstract class that inherits from Multiverse
/// </summary>
public class Singularity : Multiverse
{
	// starting layout for the board.
	Board board;

	// Since there is only one board, dont even consider the input coordinate.
	// Multiverses are only going to be sampling from the hyperVector anyway
	// This override can even handle null hypervectors.
	/// <summary>
	/// For a singularity, the coordinate is never checked. Could even be
	/// passed a null reference without issue. Returns the only board
	/// </summary>
	public override Board GetBoard(HyperVector coordinate)
	{
		return board;
	}
	/// <summary>
	/// Wraps the single board in a list for polymorphism
	/// </summary>
	public override List<Board> GetBoards()
	{
		return new List<Board>() { board };
	}

}
