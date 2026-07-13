using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasMoved : Condition
{
	public override bool IsSatisfied(Piece from, HyperVector hyperVector, Piece to, Multiverse multiverse)
	{
		return !from.hasMoved;
	}
}
