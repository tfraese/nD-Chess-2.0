using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SetType
{
	Container,
	Rider,
	Permutable,
	Directional,
	Explicit,
}

/*
 * A moveset is a container class for movement vectors, as well as conditions
 * and special information needed to generate the moves at runtime. A Move
 * Vector is an incomplete component of a move. The MoveUX class contains
 * information required to display and prompt the available moves to a player
 * and the MoveInternal class will hold information required to perform and
 * reverse moves in-engine.
 */
public class MoveSet
{
	// Enum for child class identification
	// TODO: Maybe stop doing this as a practice and just attempt type casts
	//		 I'm sure the compiler just does this behind the scenes anyway
	public SetType setType;

	// TODO: Evalute whether or not this is an appropriate structure for this
	//		 At the very least rename moveVector to something else or use
	//		 NVectors instead
	public List<MoveVector> moveVectors;
	protected List<Condition> conditions;
	int range { get; set; } = -1;

	/// <summary>
	/// Given a board layout, generate the movement vectors for a moveset
	/// within a particular game. The base virtual function initializes
	/// the list of move vectors and should always be called first within
	/// overrides.
	/// </summary>
	public virtual void Generate(BoardLayout layout)
	{
		if (moveVectors == null) { moveVectors = new List<MoveVector>(); }
	}
	// Clear all generated moves.
	public virtual void Clear()
	{
		moveVectors?.Clear();	
	}
}
