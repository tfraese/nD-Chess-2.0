using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Piece Configuration of starting board
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
	[SerializeField] List<ListData> forwardDirections;
	[SerializeField] List<ListData> lateralDirections;
	[SerializeField] List<ListData> neutralDirections;
}
