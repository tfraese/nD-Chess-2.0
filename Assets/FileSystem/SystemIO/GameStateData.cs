using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Game Save Data
/// </summary>
[Serializable]
public class GameStateData : MonoBehaviour
{
	[SerializeField] string GameName;
	[SerializeField] GameVariantData gameVariantData;
	[SerializeField] GameSettingsData gameSettingsData;
	[SerializeField] HistoryData historyData;
}
