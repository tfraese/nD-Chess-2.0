using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * An abstract class designed to represent the current board state. Display will
 * be handled using a different class to allow for more flexible viewing of
 * the game, including transposing dimensions, swapping to 2D, and various
 * analysis tools.
 */
/// <summary>
/// Strictly abstract class for representing current board state
/// </summary>
public class Board
{
	private BoardLayout layout;
}
