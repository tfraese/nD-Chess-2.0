using System.Collections;
using System.Collections.Generic;
using TFraese;
using UnityEngine;

/// <summary>
/// Classifications for axes. Internally Forward, Lateral, and None should be
/// the only options used. Multiverse and Time will be used to annotate
/// the hyperVector of the HyperVector class.
/// </summary>
public enum Axes
{
	Forward,
	Lateral,
	None,
	Multiverse,
	Time
}

/// <summary>
/// Abstract class for defining a board layout. The Board Class holds the
/// dimensionality of itself internally, but this should be the way to
/// access that information.
/// </summary>
public class BoardLayout
{
	public int[] dimensions;
	public Axes[] axesTypes;
	public BoardTypes boardType;
	public BoardState startingState;
	private List<PieceType> piecePalette;

	/// <summary>
	/// Generates a board layout from a set of raw arrays. Unfortunately since we
	/// are supporting 4-player games, simple sign to indicate color wont work.
	/// </summary>
	public BoardLayout
	(
		int[] dimensions,
		Axes[] axesType,
		BoardTypes boardType,
		int[] pieceArray, 
		int[] pieceColors,
		List<PieceType> piecePalette
	)
	{
		this.dimensions = (int[])dimensions.Clone();
		this.axesTypes = (Axes[])axesType.Clone();
		this.boardType = boardType;
		this.piecePalette = new List<PieceType>();
		this.piecePalette.AddRange(piecePalette);

		int volume = Coordinates.Volume(dimensions);
		if (volume != pieceArray.Length || volume != pieceColors.Length)
		{
			Debug.LogError("Dimension mismatch in board layout definition");
		}

		// initializes its internal piece array when constructed through specified dimensions
		startingState = new BoardState(dimensions);
		for (int i = 0; i < startingState.pieces.Length; i++)
		{
			if (pieceArray[i] != 0)
			{
				int index = pieceArray[i] - 1;
				if (index < 0 || index >= piecePalette.Count)
				{
					continue;
				}
				PieceType type = piecePalette[index];
				int color = pieceColors[index];
				if (type == null)
				{
					continue;
				}
				Piece piece = new Piece(type, color);
				startingState.pieces[i] = piece;
			}
		}
	}

	/// <summary>
	/// Return an array of axes for move generation, specifically pawns and shogi
	/// pieces.
	/// </summary>
	public int[] GetAxes(Axes axesType)
	{
		int[] result = new int[dimensions.Length];
		for (int i = 0; i < dimensions.Length; i++)
		{
			if (axesTypes[i] == axesType)
			{
				result[i] = 1;
			}
		}
		return result;
	}
	/// <summary>
	/// Just a little helper function to prettily print out the current board
	/// layout. Takes in a width and pads strings so that in a mono-space font
	/// everything is lined up cleanly.
	/// </summary>
	public string ToString(int width)
	{
		int maxLength = "empty".Length;
		for (int i = 0; i < piecePalette.Count; i++)
		{
			PieceType pieceType = piecePalette[i];
			string name = pieceType.name;
			if (name.Length > maxLength)
			{
				maxLength = name.Length;
			}
		}
		string result = "";
		for (int i = 0; i < startingState.pieces.Length; i++)
		{
			string term = i % width == width - 1 ? ",\n" : ", ";
			Piece piece = startingState.pieces[i];
			if (piece == null)
			{
				result += "empty".PadRight(maxLength + 1) + term;
				continue;
			}
			string name = piece.type.name.PadRight(maxLength + 1);
			result += name + term;
		}
		return result;
	}

}
