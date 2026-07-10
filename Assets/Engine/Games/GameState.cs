using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Investigate whether the seperation between Game.cs and GameState.cs
// is really neccesary

/// <summary>
/// Represents the current state of the game, the multiverse contained within,
/// which players are joined in at any given time, etc.
/// </summary>
public class GameState
{
	Dictionary<Player, int> playerList = new Dictionary<Player, int>();
	public Multiverse multiverse;
	float[] timers;

	public GameState(GameVariant variant, GameSettings settings)
	{
		// Setup some house keeping information
		timers = new float[variant.playerCount]; 
		for (int i = 0; i < timers.Length; i++)
		{
			timers[i] = settings.clockTime;
		}

		// The layout holds the board type, and a raw Piece[] array representing a
		// starting state, as well as other info that will be used for the move gen
		BoardLayout layout = variant.boardLayout;

		// Using just a layout we can create a board to use as a template. Once
		// created the starting state is encoded into this template board and
		// will be passed on to any new boards
		Board templateBoard = Board.Create(layout);

		// and now that we have a board type we can instantiate a new multiverse with
		// that board as a template.
		multiverse = Multiverse.Create(variant.multiverseType, templateBoard);
	
		// TODO: Find a way to encode a set of histories and repeating boards to allow
		// for things like turn 0 and multi-timeline setups.
	}

	/// <summary>
	/// Change a player's timer by the set amount in seconds
	/// </summary>
	public void DeltaTime(Player player, float amount)
	{
		int playerID = playerList[player];
		bool timerSetup = timers != null;

		// early terminates to avoid null reference on timers.Length
		bool inBounds = (timerSetup) && (playerID != -1) && (playerID < timers.Length);
		if (inBounds)
		{
			timers[playerID] += amount;
		}
	}

	/// <summary>
	/// Set a player's timer to the set amount in seconds
	/// </summary>
	public void SetTime(Player player, float amount)
	{
		int playerID = playerList[player];
		bool timerSetup = timers != null;

		// early terminates to avoid null reference on timers.Length
		bool inBounds = (timerSetup) && (playerID != -1) && (playerID < timers.Length);
		if (inBounds)
		{
			timers[playerID] = amount;
		}
	}

}
