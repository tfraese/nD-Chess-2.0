using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A Child class of MoveSet designed to contain lists of other sub-movesets
/// and compose those into more complicated ones.
/// </summary>
public class ContainerSet : MoveSet
{
	List<MoveSet> subSets;
	public ContainerSet()
	{
		subSets = new List<MoveSet>();
	}
	public override void Generate(BoardLayout layout)
	{
		// note we dont have to call base.Generate() because container types dont
		// actually have any moves of their own.
		foreach (var moveSet in subSets)
		{
			moveSet.Generate(layout);
		}
	}
	/// <summary>
	/// Remove all moves from subset lists
	/// </summary>
	public override void Clear()
	{
		foreach (var moveSet in subSets)
		{
			moveSet.Clear();
		}
	}
}
