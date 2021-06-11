using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    
    public Dice dice;
    public Button buton;
    public Image image;
    public Text titlu;
    public Text finish;
    public Manager manager;

    void Start()
    {
        dice.gameObject.SetActive(false);
        finish.gameObject.SetActive(false);
        buton.onClick.AddListener(TaskOnClick);
    }

    public void FinishScreen()
    {
        image.gameObject.SetActive(true);
        finish.gameObject.SetActive(true);
        dice.gameObject.SetActive(false);
    }

    public void TaskOnClick()
    {
        image.gameObject.SetActive(false);
        dice.gameObject.SetActive(true);
        buton.gameObject.SetActive(false);
        titlu.gameObject.SetActive(false);
        manager.SetStonesActive();
        manager.LoadMap();
    }

}
