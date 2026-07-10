using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * At runtime, the piece manager will enumerate all dev-defined pieces as well
 * as previously generated user defined pieces, and collect / construct the
 * following piece information:
 * 
 * Piece 3D model, core generation info (n-agonals, forward / backwards rules,
 * permutable movements, etc)
 * 
 * The above information will be stored in unity prefabs, and the piece manager
 * will have the power to generate new prefabs facilitate easier piece creation.
 * 
 * Upon selection and loading of a board, the MoveSetManager class will call
 * back to this information and use it to construct a set of movesets specific
 * to the selected board.
 */

/// <summary>
/// Manages piece assets and definitions. Does not generate movesets. Persistent Scene Object
/// </summary>
public class PieceManager : MonoBehaviour
{
	public static PieceManager singleton;

	List<PieceType> pieces;
	
	// keep a dictionary of pieces that we can use to quickly find the index of
	// said piece in the list.
	Dictionary<PieceType, int> pieceDictionary;

	#region Initialization
	// being a monobehavior, this will be present in the scene, and use a singleton
	// for static calls
	private void Awake()
	{
		if (singleton == null) singleton = this;
		else DestroyImmediate(this.gameObject);
	}
	#endregion

	#region Validation
	private bool IsSingleton()
	{
		return singleton != null && singleton == this;
	}
	/// <summary>
	/// Compares piece dictionary to piece list. Returns false if any piece is
	/// null, not in the dictionary, or dictionary entry does not match its
	/// list index. Leaves both untouched.
	/// </summary>
	private bool ValidateDictionary()
	{
		if (pieces.Count != pieceDictionary.Count) return false;
		for (int i = 0; i < pieces.Count; i++)
		{
			PieceType p = pieces[i];
			if (p == null) return false;

			int p_enum = GetPieceIndex(p);
			if (p_enum == -1) return false;

			if (p_enum != i) return false;
		}
		return true;
	}
	
	#endregion

	#region Piece Management
	public void AddPiece(PieceType piece)
	{
		pieceDictionary.Add(piece, pieces.Count);
		pieces.Add(piece);
	}

	/// <summary>
	/// Removes piece by piece reference. calls functions that perform null and
	/// in-dictionary checks.
	/// </summary>
	public void RemovePiece(PieceType piece)
	{
		// int to store the index of the supplied piece
		int pieceEnum = GetPieceIndex(piece);

		// if the null check found a valid piece, try to access the dictionary
		if (pieceEnum != -1)
		{
			pieceDictionary.Remove(piece);
			pieces.RemoveAt(pieceEnum);
		}
	}

	/// <summary>
	/// Returns -1 upon errors, and logs details to the Debug Warning log.
	/// </summary>
	public int GetPieceIndex(PieceType piece)
	{
		int pieceEnum = 0;
		// piece null check
		if (piece == null)
		{
			Debug.LogWarning("PieceManager.RemovePiece(piece) fed null piece");
			return -1;
		}
		// if the null check found a valid piece, try to access the dictionary
		if (pieceDictionary.TryGetValue(piece, out pieceEnum))
		{
			return pieceEnum;
		}
		// if we couldnt find it return error (-1)
		else
		{
			Debug.LogWarning($"PieceManager was unable to find {piece.name} to remove");
			return -1;
		}
	}
	#endregion
}
