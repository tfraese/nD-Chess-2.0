using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Information about board the game is played on (no layout)
/// </summary>
[Serializable]
public class BoardData
{
	[SerializeField] public string boardName;
	[SerializeField] public string boardKey; // Hash value
	[SerializeField] public int boardType;
	[SerializeField] public List<int> dimensions;
}
