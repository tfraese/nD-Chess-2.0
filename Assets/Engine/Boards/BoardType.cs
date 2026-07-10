using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum BoardTypes
{
	Cartesian,
	Toroidal,
	HyperToroidal,
	Klein
}

public class BoardType
{
	public string boardName;
	BoardType boardType;
	protected int[] dimensions;
}
