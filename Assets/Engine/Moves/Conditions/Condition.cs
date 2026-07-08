using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	int conditionType { get; }

	public virtual bool IsSatisfied(MoveInternal move, BoardState boardState)
	{
		Debug.LogWarning("IsSatisfied() for Condition not implemented");
		return true;
	}

}
