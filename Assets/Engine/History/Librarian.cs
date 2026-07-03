using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A librarian is the uppermost tier of the games history management. It
 * is responsible for the management, reception, and reconciliation of
 * histories from various historians, including from the server and any
 * remote clients.
 * 
 * A Historian is responsible for the creation and changing of histories.
 *
 * Histories are composed of a context (game variant, game settings, etc.), and
 * actions that were performed in that specific context. Actions are divided
 * into two categories, Moves, and Game Actions.
 * 
 * Moves are composed of Commands. See Move.cs for more information.
 * 
 * Actions are their own category, including start game, submit turn, forfeit
 * game, leave seat, join seat, accept opponent leaving as forfeit,
 * accept incoming spectator as new opponent, etc.
 * 
 * Throughout the game, the historian records and alters a history comprised of
 * actions. All historians will update the historian with changes to their
 * personal history. The librarian will keep track of all clients histories,
 * call to file management to save or load histories and pass them in to be
 * applied to the current board state, as well as keep a recovery history so
 * that in the event of a crash or disconnect, players will be able to reload
 * the game to the most recent state stored by the librarian. This recovery
 * state will be written to a file "recovery" periodically in the even of a
 * crash.
 */

/// <summary>
/// Manages Histories and game recovery data. Exists as persistent scene object
/// </summary>
public class Librarian : MonoBehaviour
{

}
