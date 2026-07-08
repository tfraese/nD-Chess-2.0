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
	
	public SetType setType;
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
