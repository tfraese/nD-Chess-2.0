using System.Collections;
using System.Collections.Generic;
using TFraese;
using UnityEngine;

/*
 * The piece class will contain all information neccesary to define a piece,
 * though some of this information will be fed into it at runtime or on piece
 * creation.
 * 
 * such information includes the piece's name, the model and materials it uses,
 * and all information required to generate its moveset for any given board.
 * 
 * additionally, some pieces (like "ghost" pawns) will have certain flags,
 * denoting they should be ignored for rider casting and check detection.
 * Ghost pawns are utilized to act as a flag for en passant movement, and
 * will be removed anytime someone makes a turn on that board for example.
 * 
 * See more information on move generation in MoveSetManager.cs
 */
public class PieceType
{
    public string name;
    public string key;

    public MoveSet moveSet;
    public List<PieceType> subpieces;

    // TODO: implement serializable conditions data structure, handle subpieces
    /// <summary>
    /// Constructs a piece from piece data. Can only be created from a
    /// serializable PieceData instance.
    /// </summary>
    public PieceType(PieceData data)
    {
        this.name = data.pieceName;
        this.key = data.pieceKey;
		switch (data.moveSetType)
		{
			case SetType.Container:
				this.subpieces = new List<PieceType>();
				if (data.subpieces != null)
				{
					foreach (PieceData subpieceData in data.subpieces)
					{
						subpieces.Add(new PieceType(subpieceData));
					}
				}
				break;
			case SetType.Rider:
				this.moveSet = new RiderSet(data.moveSetData0);
				break;
			case SetType.Permutable:
				this.moveSet = new PermutableSet(data.moveSetData0);
				break;
			case SetType.Directional:
				this.moveSet = new DirectionalSet(data.moveSetData0, data.moveSetData1);
				break;
			case SetType.Explicit:
				Debug.LogWarning("Explicit MoveSets not implemented");
				break;
			default:
				break;
		}
	}
    /// <summary>
    /// Constructs a serializable PieceData instance so that the piece can be
    /// saved to files and sent over the network
    /// </summary>
    public PieceData Data()
    {
        PieceData data = new PieceData();
        data.pieceName = this.name;
        data.pieceKey = this.key;
        switch (this.moveSet.setType)
        {
            case SetType.Container:
                data.subpieces = new List<PieceData>();
                if (subpieces != null)
                {
                    foreach (PieceType subpiece in subpieces)
                    {
                        data.subpieces.Add(subpiece.Data());
                    }
                }
                break;
            case SetType.Rider:
                RiderSet riderSet = (RiderSet)moveSet;
                data.moveSetData0 = riderSet.agonals;
                break;
            case SetType.Permutable:
                PermutableSet permutable = (PermutableSet)moveSet;
                data.moveSetData0 = permutable.permutables;
                break;
            case SetType.Directional:
                DirectionalSet directional = (DirectionalSet)moveSet;
                data.moveSetData0 = directional.forwardOffsets;
                data.moveSetData1 = directional.lateralOffsets;
                break;
            case SetType.Explicit:
                Debug.LogWarning("Explicit MoveSets not implemented");
                break;
            default:
                break;
        }
        return data;
    }

    /// <summary>
    /// Generates the vector offsets for a piece's base moveset. Requires info
    /// about the board layout for directional pieces.
    /// </summary>
    public void Generate(Game game)
    {
        BoardLayout layout = game.variant.boardLayout;
        if (moveSet != null)
        {
            moveSet.Generate(game);
        }
        if (subpieces != null)
        {
            foreach (PieceType subpiece in subpieces)
            {
                subpiece.Generate(game);
            }
        }
    }
}
