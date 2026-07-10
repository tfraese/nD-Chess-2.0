using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Flags
{
	Castle,
	EnPassant,
	DoublePawn
}

// TODO: Consider removing the idea of Turns -> Moves -> Commands and
// implement MoveData instead for serialization.
/// <summary>
/// Stores information neccesary to store moves to be selected by the user.
/// </summary>
public class MoveUX
{
	// A piece is easily accesible by its HyperRay, so piece should come
	// first whenever being stored within dictionaries.
	public Dictionary<Piece, HyperRay> piecesMoved;

	// The piece that initialized the move
	public Piece provokingPiece;

	// Flags to filter based on game rules.
	// TODO: Flesh out what the needs for this would be.
	List<Flags> flags;
}
