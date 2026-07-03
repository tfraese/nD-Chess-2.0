using System.Collections;
using System.Collections.Generic;
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

/// <summary>
/// Parent class for multiverse types. Moves are applied to the multiverse, and
/// the multiverse handles changing boards and branching timelines.
/// </summary>
public class Multiverse : MonoBehaviour
{

}
