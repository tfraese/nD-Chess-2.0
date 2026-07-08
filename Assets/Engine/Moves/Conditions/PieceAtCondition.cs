using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceAtCondition : Condition
{
	List<int[]> riderDirection;
	List<int[]> offsets;
	List<Piece> pieces;

	public override bool IsSatisfied(MoveInternal move, BoardState boardState)
	{
		Debug.LogWarning("IsSatisfied() for PieceAtCondition not implemented");
		return base.IsSatisfied(move, boardState);
	}
}
