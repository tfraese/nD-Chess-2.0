using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Investigate whether or not to use this program structure as described,
// or if a different object or strucure would be more appropriate

/// <summary>
/// controls enumeration of game variants, as well as loading of game variant
/// with settings in preparation for room instance creation. Persistent Scene Object.
/// </summary>
public class GameManager : MonoBehaviour
{
	List<GameVariant> variants;
	GameSettings currentSettings;
}
