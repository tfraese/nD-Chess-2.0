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
	// Consider possibility of using an Enum instead of an object. Commands in
	// particular will need to be serialized and reconstructed over the network,
	// so it may be worth it to keep things simple.
	PieceType pieceFrom;
	PieceType pieceTo;

	// TODO: Enumerate various options
	int teamFrom;
	int teamTo;

	HyperVector coordinateFrom;
	HyperVector coordinateTo;
}
