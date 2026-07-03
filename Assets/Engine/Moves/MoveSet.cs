using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A moveset is split into two components: runtime information required for
 * generation (n-agonals, permutables, conditionals, sub-movesets) [see
 * MoveSetManager.cs for more detail] as well as the actual vector offsets
 * generated at board loading specific to that boards layout.
 */
public class MoveSet
{
	public List<MoveSet> subMoveSets;
	public List<Move> moves;

	public MoveSet()
	{
		subMoveSets = new List<MoveSet>();
	}
}
