using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * A move represents all information needed to both perform and undo any change
 * in boardstate. Moves are consisted of commands, which apply individual
 * changes to a board.
 * 
 * The queening of a pawn, for example, would be composed
 * of two commands, one to move the pawn to the end rank, and another that
 * replaces the same pawn with a queen. undoing the move would then consist of
 * replacing the queen with the pawn, and then moving that pawn back to its
 * initial position.
 *
 * For networking purposes, moves will contain their own method to convert to
 * an array, which a serializer class will then convert into rpc-friendly
 * format.
 * 
 * Stored in move sets, however moveset iterations will not have pieceFrom or
 * piece to information encoded.
 */

/// <summary>
/// Represents a game move. Consists of commands, which represent changes to
/// the board.
/// </summary>
public class MoveInternal : GameAction
{
    List<Condition> conditions;
    /*
     * Follow-ups allow storage of optional moves to be selected from after the
     * first move has executed. ex. Pawn moves once in w direction. An optional
     * follow-up would be to move in the y direction, if allowing split-forward
     * double move. Some Tai and Taikyouku shogi pieces require this as well.
     */
    List<MoveInternal> followUps;

    // Pawn promotion is mandatory in both shogi and chess. Other shogi pro-
    // motions, and possibly toroidal pawn chess sometimes optional.
    bool isFollowUpMandatory;

    // simplest component of a move. Contains piece from, coordinate from,
    // coordinate to, color from, color to. Fully reversible.
    List<Command> commands = new List<Command>();
}
