using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Parent class for actions like Move, SubmitTurn, Forfeit, OfferDraw, etc.
/// </summary>
public class GameAction
{
	// converts the move into a serialized array for networking
	public virtual int[] ToArray()
	{
		Debug.LogWarning("Move.ToArray() not yet implemented");
		return null;
	}

	// re-generates the move from an array
	public virtual void FromArray(int[] array)
	{

	}
}
