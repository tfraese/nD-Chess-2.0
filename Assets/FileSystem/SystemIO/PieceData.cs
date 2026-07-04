using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PieceData : MonoBehaviour
{
	[SerializeField] string pieceName;
	[SerializeField] string pieceKey; // Hash value
	[SerializeField] List<ListData> agonals;
	[SerializeField] List<ListData> permutables;
	[SerializeField] List<ListData> directionals;
	[SerializeField] List<string> subpieceKeys;
}
