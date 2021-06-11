using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    Transform[] childObjects;
    Renderer[] cubes;
    public List<Transform> childNodeList = new List<Transform>();
    public Material routeMat;
    int childNr;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        
        FillNodes();

        for (int i = 0; i < childNodeList.Count; i++)
        {
            Vector3 currentPos = childNodeList[i].position;
            if(i > 0)
            {
                Vector3 prevPos = childNodeList[i - 1].position;
                Gizmos.DrawLine(prevPos, currentPos);
            }
        }
    }

    void FillNodes()
    {
        childNr = -2;
        childNodeList.Clear();

        childObjects = GetComponentsInChildren<Transform>();

        foreach(Transform child in childObjects)
        {
            if((child != this.transform) && (childNr % 5 == 0) || (childNr == -1))
            {
                childNodeList.Add(child);
            }
            childNr++;
        }
    }

    public void ResetRouteMaterial()
    {
        childNr = -2;
        cubes = GetComponentsInChildren<Renderer>();

        foreach (Renderer child in cubes)
        {
            if ((childNr % 5 == 0) || (childNr == -1) || (childNr == -2))
            {
                child.material = routeMat;
            }
            childNr++;
        }
    }
}
