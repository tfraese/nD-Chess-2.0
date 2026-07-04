using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * An MVTime multiverse is analogous to other time travel chess games. It
 * consists of various timelines, that advance when moves are made on them and
 * split off new timelines when a move is attempted on a pre-defined point in 
 * the timelines past.
 * 
 * Structurally, one mathematical trick being used is the folding of the
 * coordinate system. Let n be a negative integer, and m a positive integer.
 * the "folded" or "flattened" coordinate of n will be the |n|th odd number,
 * and the flattened coordinate of m will be the |n+1|th even number (skipping
 * zero). This allows serialization of the multiverse state into an array with
 * non-negative indices.
 * 
 * The class is further divided into individual timeline classes. Some design
 * considerations will require each timeline to have its own base layout state,
 * and turn zero will require game variants to support advancing the timeline
 * without actually performing a move.
 * 
 * TODO: Factor this into data serialization and game state data BEFORE
 * generating base game files like default boards and variants.
 */

/// <summary>
/// Abstract class representing a 2D multiverse (m, t)
/// </summary>
public class MVTime : Multiverse
{
	private List<Timeline> lines;
}
