using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract class for storing a start and end point of a move, capable of
/// representing moves across timelines and turns.
/// </summary>
public class HyperRay
{
    public HyperVector from;
    public HyperVector to;
    public HyperRay(HyperVector from, HyperVector to)
	{
		this.from = from;
		this.to = to;
	}
}
