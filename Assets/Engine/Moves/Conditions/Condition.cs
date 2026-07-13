using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Investigate Necessity of this
enum Conditions
{
	None,
	Capturing,
	NotCapturing,
	NotMoved,
	NeverThreatened,
	PathNotThreatened,
	PieceAtOffset,
	PieceInDirection,
	UnmovedPieceInDirection,
	UniqueInFile
}

public class Condition
{
	/// <summary>
	/// Given information about a piece's move and multiverse state, determine
	/// whether condition is met.
	/// </summary>
	public virtual bool IsSatisfied(Piece from, HyperVector hyperVector, Piece to, Multiverse multiverse)
	{
		Debug.LogWarning("IsSatisfied() for Condition not implemented");
		return true;
	}
}
