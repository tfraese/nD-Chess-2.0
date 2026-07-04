using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Information about board the game is played on (no layout)
/// </summary>
[Serializable]
public class BoardData : MonoBehaviour
{
	enum BoardTypes { Cartesian, Toroidal, HyperTorroidal, Klein, XKCD }

	[SerializeField] string boardName;
	[SerializeField] string boardKey; // Hash value
	[SerializeField] int boardType;
	[SerializeField] List<int> dimensions;
	[SerializeField] List<int> bounds;
	[SerializeField] List<float> offsets;
	[SerializeField] List<int> transpositionArray;
	[SerializeField] List<string> dimensionLabels;
}
