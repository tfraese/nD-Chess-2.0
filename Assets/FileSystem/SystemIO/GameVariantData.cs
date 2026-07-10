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
    [SerializeField] string variantName;
    [SerializeField] string variantKey; // Hash value
    [SerializeField] BoardLayoutData boardLayoutData;
}
