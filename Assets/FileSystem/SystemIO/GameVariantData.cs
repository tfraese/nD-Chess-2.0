using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Board and layout game uses
/// </summary>
[Serializable]
public class GameVariantData
{
    [SerializeField] public string variantName;
    [SerializeField] public string variantKey; // Hash value
    [SerializeField] public int multiverseType;
    [SerializeField] public BoardLayoutData boardLayoutData;
}
