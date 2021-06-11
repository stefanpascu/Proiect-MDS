using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRotation : MonoBehaviour
{
    public Stone pion;
    int index = 0;
    int ok = 0;

    void OnCollisionExit()
    {
        ok = 1;
    }


    void FixedUpdate()
    {
        if (ok == 1)
        {
            index = index + 3;
            this.transform.Rotate(new Vector3(3f, 0f, 0f));
            if (index == 90)
            {
                ok = 0;
                index = 0;
            }
        }
    }
}
