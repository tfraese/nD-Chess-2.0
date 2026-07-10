using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract class storing various game settings. Things like
/// castling interpretations, force promotion or demotion of
/// things like Princess <-> Queen, Split pawn forward motion, etc.
/// </summary>
public class GameSettings
{
    bool analysisMode;

	public float clockTime; // clock starting time
	float clockIncrease; // time clock increases on move

    bool allowSpectator; // TODO: Move to Lobby settings
	int spectatorCount; // TODO: Move to Lobby settings

    bool allowHotSeat; // Can spectators tag in as players
    bool allowSpectatorCursor; // should probably be lobby settings
    bool allowSpectatorAnalysis; // also lobby settings
}
