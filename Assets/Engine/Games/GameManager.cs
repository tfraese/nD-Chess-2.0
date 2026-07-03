using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// controls enumeration of game variants, as well as loading of game variant
/// with settings in preparation for room instance creation. Persistent Scene Object.
/// </summary>
public class GameManager : MonoBehaviour
{
	List<GameVariant> variants;
	GameSettings currentSettings;
}
