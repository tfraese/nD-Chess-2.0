using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Handles piece moveset generation through static calls
 * 
 * Moveset generation so far is split among the following parts
 * 
 * n-agonal rider movement: and n-agonal refers to a piece movement of a rider
 * across n directions at once. A rook is a 1-agonal or monagonal rider, a
 * bishop is a 2-agonal or diagonal rider.
 * 
 * Permutables: A permutable is a set of leaping piece movements constructed
 * from permutations of given jump distances. A knight is a permutable with the
 * base generation being (2,1), or a (1,2)-permutable.
 * 
 * Directionals: Directional movements are dependent on both color, and
 * the distinction between "forward" and "lateral" movements. Forward directions
 * will be reversed based on piece color, whereas lateral movements will not.
 * A Pawn for example, is in 2D a (y)(0,1)-directional for base movement,
 * and a conditional (see below) (y)(1,1)-directional for capturing. A Shogi
 * Knight would be a (y)(1,2)-directional.
 * 
 * Note: A forward direction is one that gets you closer or further to the
 * enemies camp, a lateral direction leaves that distance unchanged.
 * 
 * Conditionals: Conditionals apply a requirement to various sub-movesets such
 * as requiring capture, requiring un-moved, requiring unobstructed, castling,
 * etc.
 * 
 * Submoveset: In addition to defining pieces individually, each moveset can
 * have any number of sub-movesets. A queen for example could be constructed
 * or an n-D board with purely a list of m-agonal {m | 1 <= m <= n} subpieces,
 * of single m-agonal riders.
 * 
 * A Shogi Gold General could be constructed with a (1,0) permutable, and a
 * (y)(1,1) directional.
 * 
 * Likewise this allows conditionals to be applied to only specific sub-movesets
 * in order to facilitate cleaner definitions of pawns, which will consist of
 */
public class MoveSetManager
{
    
}
