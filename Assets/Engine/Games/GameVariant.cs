using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A Game Variant holds all information neccesary for playing any variant of
 * chess game.
 * 
 * Key components are the base board, whether or not the game uses multiverse
 * time travel, number of players, specific board layouts (which contains
 * direction defintions), etc.
 * 
 * Game information will be stored as a prefab for development, but will 
 * eventually have serialization methods to save to files for user defined
 * variants.
 */

// FourFoldMultiverse is a four player version of multiverse time travel. White
// White and player 3 split vertically, Black and player 4 split horizontally.
enum MultiverseTypes {Singularity, MultiverseTimeTravel, FourFoldMultiverse}

/// <summary>
/// Defines game variant, including board type, players, and multiverse.
/// Game manager will load settings like clock and modifier rules. Does not
/// change past startup. Stored as Prefab with serialization options.
/// </summary>
public class GameVariant : MonoBehaviour
{
    int playerCount;

    Board gameBoard;
    BoardLayout boardLayout;

    int multiverseType;

}
