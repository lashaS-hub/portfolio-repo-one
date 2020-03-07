using System.Collections;
using System.CodeDom;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "figure_name", menuName = "Scriptable/TargetPositions")]
public class TargetPositions : ScriptableObject
{
    // public int a;
    public int[] indexes;


    public int[,] TargetBoxLocations(int i, int j)
    {
        var ij = new int[indexes.Length / 2, 2];
        for (int k = 0; k < indexes.Length; k++)
        {
            if (k % 2 == 0)
            {
                if (k == 0)
                {
                    ij[k, 0] = i + indexes[k];
                    ij[k, 1] = j + indexes[k + 1];
                }
                else
                {
                    ij[k / 2, 0] = i + indexes[k];
                    ij[k / 2, 1] = j + indexes[k + 1];
                }
            }
        }
        return ij;
    }
}
