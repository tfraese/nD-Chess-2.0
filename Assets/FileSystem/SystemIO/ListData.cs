using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generic serializable wrapper class for lists. Includes field for a label as
/// well.
/// </summary>
[Serializable]
public class ListData
{
	[SerializeField] public string listLabel;
	[SerializeField] public List<int> list;
}
