using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The base component of moves, a fully reversible change in a board. Moves
 * generally consist of one command, but queening, en passant, double pawn
 * motions, etc. consist of multiple. See Move.cs for more info.
 */

/// <summary>
/// Base component of a move
/// </summary>
public class Command
{
	Piece pieceFrom;
	Piece pieceTo;

	int teamFrom;
	int teamTo;

	int[] coordinateFrom;
	int[] coordinateTo;
}
