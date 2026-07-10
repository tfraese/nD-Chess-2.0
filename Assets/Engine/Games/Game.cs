using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Investigate the neccesity of this class. Like, i know i'll need a game
// data class to serialize a games entire history and current state but i don't
// know if this is all that neccesary.
public class Game
{
    GameVariant variant;
    GameSettings settings;
    GameState state;

    public Game(GameVariant variant, GameSettings settings)
    {
        GameState state = new GameState(variant, settings);
    }


}
