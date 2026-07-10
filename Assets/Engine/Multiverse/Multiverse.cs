using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
/*
 * A Multiverse exists as a wrapper for accessing and managing the game state.
 * Multiverses are divided into 3 categories so far,
 * 
 * Singularity: A single chessboard acting as a traditional chess game.
 * Changes to board states are applied to the existing board, and by default
 * moves submit automatically.
 * 
 * Multiverse Time Travel: A 2D multiverse containing branching timelines of
 * individual boards. Changes are applied to the newest board in the timeline
 * the change was made to. Traveling back splits off a new timeline, with the
 * direction being determined by the color of the player branching.
 * 
 * Fourfold Multiverse: A 3D multiverse designed for 4 players. Each player's
 * timelines branch in a different direction. See Fourfold.cs for more details
 */


// FourFoldMultiverse is a four player version of multiverse time travel. White
// White and player 3 split vertically, Black and player 4 split horizontally.
public enum MultiverseTypes
{ 
	Singularity,
	MultiverseTimeTravel,
	FourFoldMultiverse
}

/// <summary>
/// Parent class for multiverse types. Acts as wrapper for regular boards
/// (singularity) and allows for timeline branching on other modes
/// </summary>
public class Multiverse
{
	// A template board class that will be utilized for getting general information
	// common to all boards within the multiverse, and used for initial setup.
	public Board templateBoard;

	/// <summary>
	/// Wrapper for switch case for constructing child classes from the enum of types
	/// </summary>
	public static Multiverse Create(MultiverseTypes type, Board templateBoard)
	{
		switch (type)
		{
			case MultiverseTypes.Singularity:
				return new Singularity(templateBoard);
			case MultiverseTypes.MultiverseTimeTravel:
				return new MVTime(templateBoard);
			case MultiverseTypes.FourFoldMultiverse:
				return new Fourfold(templateBoard);
			default:
				Debug.LogWarning("Falling back to singularity");
				return new Singularity(templateBoard);
		}
	}

	/// <summary>
	/// Enumerate all available moves within the multiverse
	/// </summary>
	public List<MoveUX> GetMoves()
	{
		Debug.LogWarning("Multiverse.GetMoves() Not implemented");
		List<MoveUX> result = new List<MoveUX>();
		return null;
	}
	/// <summary>
	/// Return all non-empty pieces, as well as the coordinates they reside within.
	/// </summary>
	public Dictionary<Piece, HyperVector> GetPieces()
	{
		Dictionary<Piece, HyperVector> pieces = new Dictionary<Piece, HyperVector>();
		List<Board> boards = GetBoards();
		foreach (Board board in boards)
		{
			Dictionary<Piece, HyperVector> boardPieces = board.GetPieces();
			foreach (var (key, value) in boardPieces)
			{
				pieces.Add(key, value);
			}
		}
		return pieces;
	}
	// TODO: Implementation will depend on the data structure used to represent
	//		 multiverse layouts. Impelement this
	public virtual List<Board> GetBoards()
	{
		Debug.LogWarning("MVTime GetBoard() not implemented");
		return null;
	}
	public virtual Board GetBoard(HyperVector coordinate)
	{
		Debug.LogWarning("MVTime GetBoard(HyperVector) not implemented");
		return null;
	}

}
