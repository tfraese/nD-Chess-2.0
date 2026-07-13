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
	public override List<MoveUX> ConstructMoves(Piece piece, HyperVector coordinate, Multiverse multiverse)
	{
		List<MoveUX> result = new List<MoveUX>();
		if (subSets != null)
		{
			foreach (MoveSet subSet in subSets)
			{
				// TODO: Evaluate whether or not we want to seperate submoves into their own lists.
				List<MoveUX> submoves = subSet.ConstructMoves(piece, coordinate, multiverse);
				result.AddRange(submoves);
			}
		}
		return result;
	}
	public override void Generate(Game game)
	{
		// note we dont have to call base.Generate() because container types dont
		// actually have any moves of their own.
		foreach (var moveSet in subSets)
		{
			moveSet.Generate(game);
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
