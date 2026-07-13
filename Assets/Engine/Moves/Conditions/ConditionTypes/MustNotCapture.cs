using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MustNotCapture : Condition
{
	public override bool IsSatisfied(Piece from, HyperVector hyperVector, Piece to, Multiverse multiverse)
	{
		return to == null || to.isGhost;
	}
}
