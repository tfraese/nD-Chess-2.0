using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Consider removing the idea of Turns -> Moves -> Commands and
// implement MoveData instead for serialization.

// TODO: Consider handling logic for null from and to hypervectors, representing
// Adding a piece (promotion) and removing a piece (capture) from the board in
// piecesMoved

/// <summary>
/// Stores information neccesary to store moves to be selected by the user.
/// Note that without a state this does not retain enough information to undo
/// the move, and it omits data like promotion and captures.
/// </summary>
public class MoveUX
{
	// A piece is easily accesible by its HyperRay, so piece should come
	// first whenever being stored within dictionaries.

	// TODO: Evaluate whether the dictionary structure is really neccesary
	// or if i can use an ordered structure
	public Dictionary<Piece, HyperRay> piecesMoved;

	// The piece that initialized the move
	public Piece provokingPiece;

	// TODO: Standardize a constructor. I think the dictionary and provoking
	// piece should honestly be the only inputs, because MoveSet is
	// generating these anyway. For now while riskier, his is cleaner 
	// when invoked.
	public MoveUX(Piece piece, HyperVector from, HyperVector to)
	{
		piecesMoved = new Dictionary<Piece, HyperRay>();
		piecesMoved.Add(piece, new HyperRay(from, to));
		provokingPiece = piece;
	}
	public MoveUX(Piece piece, HyperRay movement)
	{
		piecesMoved = new Dictionary<Piece, HyperRay>();
		piecesMoved.Add(piece, movement);
		provokingPiece = piece;
	}
	/// <summary>
	/// Add an aditional piece and it's movement
	/// </summary>
	public void AddPiece(Piece piece, HyperVector from, HyperVector to)
	{
		AddPiece(piece, new HyperRay(from, to));
	}
	public void AddPiece(Piece piece, HyperRay movement)
	{
		piecesMoved.Add(piece, movement);
	}
}
