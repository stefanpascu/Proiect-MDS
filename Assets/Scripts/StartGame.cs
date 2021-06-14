using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;


public class StartGame : MonoBehaviour
{
    public GameObject pfp;
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


    public void TaskOnClick()
    {
        image.gameObject.SetActive(false);
        dice.gameObject.SetActive(true);
        buton.gameObject.SetActive(false);
        titlu.gameObject.SetActive(false);
        manager.SetStonesActive();
        manager.LoadMap();
        StartCoroutine(IncrementGameStarted());
    }

    public void GoToProfile()
    {
        SceneManager.LoadScene(3);
    }

    IEnumerator IncrementGameStarted()
    {
        WWWForm form = new WWWForm();
        form.AddField("userId", DBManager.userId.ToString());
        form.AddField("username", DBManager.username);
        UnityWebRequest www = UnityWebRequest.Post("http://localhost/mds_db/gameStart.php", form);
        yield return www.SendWebRequest();

        if (www.downloadHandler.text[0] == '0')
        {
            //ProfilePage.gamesStarted.text = "Games Started: " + www.downloadHandler.text.Split('\t')[1];
            DBManager.gamesStarted = int.Parse(www.downloadHandler.text.Split('\t')[1]);
            Debug.Log("Game Started Succesfully");
        }
        else
        {
            Debug.Log("Game Loading Failed. Error #" + www.downloadHandler.text);
        }
    }




}
