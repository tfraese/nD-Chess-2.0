using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract class represent an individual piece on a board
/// </summary>
public class Piece
{
    public Piece(PieceType type, int color)
    {
        this.type = type;
        this.color = color;
        hasMoved = false;
        isGhost = false;
    }

    public PieceType type;
    public int color;
    public bool hasMoved;
    public bool isGhost; // used to implement en passant
}
