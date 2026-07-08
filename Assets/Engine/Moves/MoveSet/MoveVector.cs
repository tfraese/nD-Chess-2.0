using System.Collections;
using System.Collections.Generic;
using TFraese;
using UnityEngine;

public class MoveVector
{
	public MoveVector followUpVectors;
	int[] vector;
	public MoveVector(int[] vector)
	{
		this.vector = (int[])vector.Clone();
	}
	public override string ToString()
	{
		return "\n" + Arrays.ToString(vector);
	}
}
