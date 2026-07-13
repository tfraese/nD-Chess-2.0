using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TFraese;

/*
 * An abstract class designed to represent the current board state. Display will
 * be handled using a different class to allow for more flexible viewing of
 * the game, including transposing dimensions, swapping to 2D, and various
 * analysis tools.
 */
/// <summary>
/// Strictly abstract class for representing current board state
/// </summary>
public class BoardState
{
	// for internal use only. Should always be assigned in constructors and
	// remain unchanged after. Access dimensions from layout
	private int[] dimensions;

	// TODO: Evaluate wether or not this can be made a struct instead of a class
	// Array of piece reference instances. Lightweight Class since this will
	// be instantiated a lot.
	public Piece[] pieces;

	public Piece GetPiece(NVector nVector)
	{
		return GetPiece(nVector.array);
	}
	// TODO: Evaluate whether or not to use NVector's instead of int[]'s
	public Piece GetPiece(int[] coordinate)
	{
		// TODO: Profile calculating coordinates on the fly with modulo
		// operators vs. cacheing pre-gened coordinates in an array.
		int index = Coordinates.CoordinateToIndex(coordinate, dimensions);
		return GetPiece(index);
	}
	public Piece GetPiece(int index)
	{
		return pieces[index];
	}
	public void SetPiece(int[] coordinate, Piece piece)
	{
		// TODO: Profile calculating coordinates on the fly with modulo
		// operators vs. cacheing pre-gened coordinates in an array.
		int index = Coordinates.CoordinateToIndex(coordinate, dimensions);
		SetPiece(index, piece);
	}
	public void SetPiece(int index, Piece piece)
	{
		pieces[index] = piece;
	}
	#region Setup
	/// <summary>
	/// Generates a blank board state of the given dimensions.
	/// </summary>
	public BoardState(int[] dimensions)
	{
		this.dimensions = dimensions;
		int volume = Coordinates.Volume(dimensions);
		pieces = new Piece[volume];
	}
	/// <summary>
	/// Generates a board while setting it's state to be identical to the provided.
	/// Copies the state's data into a new object rather than assigning reference.
	/// </summary>
	public BoardState(BoardState state)
	{
		this.dimensions = (int[])state.dimensions;
		this.pieces = new Piece[state.pieces.Length];
		System.Array.Copy(state.pieces, this.pieces, state.pieces.Length);
	}
	#endregion
}
