using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Serializable piece Configuration of starting board. Holds information as
/// to what board to use, starting piece array, custom pieces, and direction
/// forwards / lateral / etc. classification.
/// </summary>
[Serializable]
public class BoardLayoutData
{
	[SerializeField] public string layoutName;
	[SerializeField] public string layoutKey; // Hash value
	[SerializeField] public BoardData boardData;
	// This is going to need a different serialization method
	[SerializeField] public List<int> boardState;
	[SerializeField] public bool customPieces;
	[SerializeField] public List<PieceData> piecePalette;
	[SerializeField] public List<ListData> directionTypes; // stores forward, lateral, etc
}
