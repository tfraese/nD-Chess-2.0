using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TFraese;

/// <summary>
/// Abstract class that holds the state of a given board.
/// </summary>
public class Board
{
	// the boards coordinate in a multiverse
	public NVector mvtimeVector;

	// TODO: Convert this into an enum that children inheriting from the board
	//		 class
	BoardType type;

	// Stores dimensionality, forward/lateral, and starting configuration info
	BoardLayout layout;

	// Board state will update over time with changes to board
	BoardState state;

	/// <summary>
	/// Construct a board with a given starting state from the supplied layout
	/// </summary>
	public Board(BoardType type, BoardLayout layout)
	{
		BoardState state = new BoardState(layout.startingState);
		this.layout = layout;
		this.type = type;
	}
	/// <summary>
	/// Construct a board with a starting state identical to the supplied board's
	/// current state. Copies the state data into a new object, not reference
	/// </summary>
	public Board(BoardType type, BoardLayout layout, BoardState state)
	{
		BoardState boardState = new BoardState(state);
		this.layout = layout;
		this.type = type;
	}
	/// <summary>
	/// Gather all non-empty pieces.
	/// </summary>
	public Dictionary<Piece, HyperVector> GetPieces()
	{
		Dictionary<Piece, HyperVector> pieces = new Dictionary<Piece, HyperVector>();
		Piece[] pieceArray = this.state.pieces;
		for (int i = 0; i < pieceArray.Length; i++)
		{
			Piece piece = pieceArray[i];
			if (piece != null)
			{
				int[] dimensions = layout.dimensions;
				// construct the pieces local board coordinate as an array of ints
				int[] coordinateArray = Coordinates.IndexToCoordinate(i, layout.dimensions);
				
				// convert that array into a proper NVector
				NVector localCoordinate = new NVector(coordinateArray);

				// Add the boards current mvtt coordinate into the coordinate information.
				// mvtime Vector can be null, and will be in the case of a singularity
				HyperVector fullCoordinate = new HyperVector(mvtimeVector, localCoordinate);
				pieces.Add(piece, fullCoordinate);
			}
		}
		return pieces;
	}


}
