using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Mainly refers to "ghost pawns," the tool pawns leave behind to facilitate
/// En passant. Ghosts are ignored in standard capture and ray casting ops.
/// </summary>
public class MustCaptureGhost : Condition
{
	public override bool IsSatisfied(Piece from, HyperVector hyperVector, Piece to, Multiverse multiverse)
	{
		return to != null && to.isGhost;
	}
}
