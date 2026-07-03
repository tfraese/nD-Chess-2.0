using System.Collections;
using System.Collections.Generic;
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
public class Piece
{
    public string name;
    
    // default constructer, may be removed later
    public Piece()
    {
        name = "default";
    }
}
