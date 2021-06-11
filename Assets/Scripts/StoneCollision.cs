using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneCollision : MonoBehaviour
{
    public Stone pion1;
    public Stone pion2;
    public Stone pion3;
    public Stone pion4;
    int pos1, pos2, pos3, pos4;
    int nr;


    public int TestCollision(int pos)
    {
        nr = 0;
        pos1 = pion1.GetNodeNumber();
        pos2 = pion2.GetNodeNumber();
        pos3 = pion3.GetNodeNumber();
        pos4 = pion4.GetNodeNumber();

        if (pos1 == pos)
        {
            nr++;
        }

        if (pos2 == pos)
        {
            nr++;
        }

        if (pos3 == pos)
        {
            nr++;
        }

        if (pos4 == pos)
        {
            nr++;
        }
        return nr;
    }

}
