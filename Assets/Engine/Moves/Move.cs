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
 */

/// <summary>
/// Represents a game move. Consists of commands, which represent changes to
/// the board.
/// </summary>
public class Move : GameAction
{
    List<Command> commands = new List<Command>();


}
