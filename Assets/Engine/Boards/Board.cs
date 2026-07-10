using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TFraese;

public enum BoardTypes
{
	Cartesian,
	Toroidal,
	HyperToroidal,
	Klein
}

/// <summary>
/// Abstract class that holds the state of a given board.
/// </summary>
public class Board
{
	public string boardName;

	// the boards coordinate in a multiverse
	public NVector mvtimeVector;

	// identifier for child classes
	BoardTypes boardType;

	// Stores dimensionality, forward/lateral, and starting configuration info
	// Even blank boards should be created with layouts including blank states
	BoardLayout layout;

	// Board state will update over time with changes to board
	protected BoardState state;

	/// <summary>
	/// Creates a new board, and copies the type, dimensions, and board state data
	/// into new objects. Copys the reference of the layout used
	/// </summary>
	/// <returns></returns>
	public Board Clone()
	{
		Board clone = Create(this.boardType, this.layout.dimensions);
		
		// since we cloned an empty board and copied its state, we must manually
		// assign the layout.
		// TODO: investigate the importance of this
		clone.layout = this.layout;
		
		// Copy the state data of the current board into its new clone.
		clone.state = new BoardState(this.state);
		return clone;
	}

	/// <summary>
	/// Create a new board with a reference to the layout supplied.
	/// </summary>
	public static Board Create(BoardLayout layout)
	{
		BoardTypes type = layout.boardType;
		switch (type)
		{
			default:
				Debug.LogWarning("Falling back to cartesian board");
				return new BoardCartesian(layout);
		}
	}
	/// <summary>
	/// Internal function to create a blank board without a layout. Is only used
	/// to skip copying the starting layout everytime a new board is instantiated
	/// </summary>
	private static Board Create(BoardTypes type, int[] dimensions)
	{
		switch (type)
		{
			default:
				Debug.LogWarning("Falling back to cartesian board");
				return new BoardCartesian(dimensions);
		}
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
