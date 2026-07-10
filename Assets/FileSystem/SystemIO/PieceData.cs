using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Serializable class for storing all data neccesary to define and generate
/// a piece's moveset. Does not generate any movesets, just defines them.
/// Passed into Piece class' constructor (Piece does generate moves on game
/// load).
/// </summary>
[Serializable]
public class PieceData
{

	[SerializeField] public string pieceName;
	[SerializeField] public string pieceKey; // Hash value
	[SerializeField] public SetType moveSetType; // Rider, Permutable, Directional, etc.
	[SerializeField] public int range; // piece max distance.
	[SerializeField] public List<int> moveSetData0;
	[SerializeField] public List<int> moveSetData1;
	[SerializeField] public List<PieceData> subpieces;

	public const int RANGE_UNLIMITED = -1;

	/// <summary>
	/// Sets the pieces data to container, a piece type reserved for containing a
	/// list of sub pieces
	/// </summary>
	public void CreateContainer(string name, List<PieceData> subpieces)
	{
		this.pieceName = name;
		this.moveSetType = SetType.Container;
		this.subpieces = subpieces;
		UpdateData();
	}
	/// <summary>
	/// Sets the pieces data to a rider, that can travel in specified directions
	/// </summary>
	public void CreateRider(string name, List<int> agonals, int range=RANGE_UNLIMITED)
	{
		CreateData(name, SetType.Rider, agonals, range);
	}
	/// <summary>
	/// Sets the pieces data to a permutable, that can travel in permutations of
	/// supplied numbers of squares in any unique single direction each.
	/// </summary>
	public void CreatePermutable(string name, List<int> permutables, int range=RANGE_UNLIMITED)
	{
		CreateData(name, SetType.Permutable, permutables, range);
	}
	/// <summary>
	/// Sets the piece data to a directional, pieces that have a set "forward"
	/// direction that they cannot backtrack from.
	/// </summary>
	public void CreateDirectional(string name, List<int> forwards, List<int> laterals=null, int range=RANGE_UNLIMITED)
	{
		CreateData(name, SetType.Directional, forwards, laterals, range);
	}
	/// <summary>
	/// Internal method that actually creates the piece. Specific methods are for
	/// readability
	/// </summary>
	private void CreateData(string name, SetType type, List<int> a, int range=RANGE_UNLIMITED)
	{
		CreateData(name, type, a, null, range);
	}
	/// <summary>
	/// Internal method that actually creates the piece. Specific methods are for
	/// readability. Second list is used for additional information like lateral
	/// offsets.
	/// </summary>
	private void CreateData(string name, SetType type, List<int> a, List<int> b, int range=RANGE_UNLIMITED)
	{
		this.pieceName = name;
		this.moveSetType = type;
		this.range = range;
		this.moveSetData0 = a;
		this.moveSetData1 = b;
		UpdateData();
	}
	/// <summary>
	/// Calculates and sets the hash, and updates the mono-behavior's name to
	/// include both hash and piece name. 
	/// </summary>
	private bool UpdateData()
	{
		bool matched = CalculateHash();
		return matched;
	}
	/// <summary>
	/// Calculate the hash of the object instance
	/// </summary>
	private bool CalculateHash()
	{
		string oldHash = this.pieceKey;
		this.pieceKey = HashFunction();
		return oldHash == this.pieceKey;
	}
	/// <summary>
	/// Define the hash function used for the class.
	/// </summary>
	private string HashFunction()
	{
		Debug.LogWarning("Hash function not yet implemented");
		return UnityEngine.Random.Range(0, int.MaxValue).ToString();
	}
}
