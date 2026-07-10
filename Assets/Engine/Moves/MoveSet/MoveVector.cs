using System.Collections;
using System.Collections.Generic;
using TFraese;
using UnityEngine;

// TODO: investigate the neccesity of this class. Probably will be replaced
// with a combination of MoveUX and HyperRay/HyperVectors
public class MoveVector
{
	// Yeah we can definitely replace these with NVectors.
	// Follow up moves should be stored in MoveUX
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
