using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents individual timeline in multiverse. Abstract class.
/// </summary>
public class Timeline
{
	// line coordinate across multiverse
	public int line;

	// origin state for this board for multi-timeline games
	private BoardLayout originLayout;

	// number of times to repeat origin state before applying history, for turn 0
	private int leadingBoards;

	// leading history to apply after leading boards, allowing for puzzle creation
	private History leadingHistory;

	private List<BoardState> boards;
}
