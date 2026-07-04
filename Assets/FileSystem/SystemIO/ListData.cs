using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ListData : MonoBehaviour
{
	[SerializeField] string listLabel;
	[SerializeField] List<int> list;
}
