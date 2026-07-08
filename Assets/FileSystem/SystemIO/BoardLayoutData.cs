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
public class BoardLayoutData : MonoBehaviour
{
	[SerializeField] string layoutName;
	[SerializeField] string layoutKey; // Hash value
	[SerializeField] string boardKey;
	[SerializeField] bool customBoard;
	[SerializeField] BoardData boardData;
	[SerializeField] List<int> boardState;
	[SerializeField] bool customPieces;
	[SerializeField] List<PieceData> piecePalette;
	[SerializeField] List<ListData> directionTypes; // stores forward, lateral, etc
}
