using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject map1;
    public GameObject map2;
    public GameObject map3;
    public Route route;
    public Stone pion1;
    public Stone pion2;
    public Stone pion3;
    public Stone pion4;
    public Dropdown nrStones;
    public Dropdown chooseMap;
    int nrPioni;
    float duration = 1.0f;
    Color color0 = Color.red;
    Color color1 = Color.blue;
    public Light lt;

    // Start is called before the first frame update

    void Start()
    {
        pion1.gameObject.SetActive(false); 
        pion2.gameObject.SetActive(false);
        pion3.gameObject.SetActive(false); 
        pion4.gameObject.SetActive(false);
    }

    public void SetNrStones()
    {

        if (nrStones.value == 0)
            nrPioni = 2;
        if (nrStones.value == 1)
            nrPioni = 3;
        if (nrStones.value == 2)
            nrPioni = 4;
    }

    public int GetNrStones()
    {
        return nrPioni;
    }

    public void SetStonesActive()
    {
        SetNrStones();
        nrStones.gameObject.SetActive(false);
        pion1.gameObject.SetActive(true);
        pion2.gameObject.SetActive(true);
        if (nrPioni == 3)
        {
            pion3.gameObject.SetActive(true);
        }
        if (nrPioni == 4)
        {
            pion3.gameObject.SetActive(true);
            pion4.gameObject.SetActive(true);
        }
    }

    public void LoadMap()
    {
        chooseMap.gameObject.SetActive(false);
        if (chooseMap.value == 0)
        {
            map1.gameObject.SetActive(true);
            route.ResetRouteMaterial();
        }
        if (chooseMap.value == 1)
            map2.gameObject.SetActive(true);
        if (chooseMap.value == 2)
            map3.gameObject.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        float t = Mathf.PingPong(Time.time, duration) / duration;
        lt.color = Color.Lerp(color0, color1, t);
    }
}
