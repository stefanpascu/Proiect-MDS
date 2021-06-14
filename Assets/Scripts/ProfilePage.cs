using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;


public class ProfilePage : MonoBehaviour
{
    public Text firstName;
    public Text lastName;
    public Text username;
    public Text gamesStarted;
    public Text gamesFinished;
    public Button delete;
    public GameObject warning;
    public GameObject warning2;
    public Button yes;
    public Button no;

    void Start()
    {
        firstName.text = DBManager.firstName;
        lastName.text = DBManager.lastName;
        username.text = "Username: " + DBManager.username;
        gamesStarted.text = "Games Started: " + DBManager.gamesStarted.ToString();
        gamesFinished.text = "Games Finished: " + DBManager.gamesFinished.ToString();
    }


    public void GoToMenu()
    {
        SceneManager.LoadScene(2);
    }

    public void ShowWarning()
    {
        warning.gameObject.SetActive(true);
    }

    public void ShowWarning2()
    {
        warning2.gameObject.SetActive(true);
    }

    public void HideWarning()
    {
        warning.gameObject.SetActive(false);
        warning2.gameObject.SetActive(false);
    }

    public void LogOut()
    {
        DBManager.LogOut();
    }

    public void DeleteUser()
    {
        StartCoroutine(Delete());
        DBManager.LogOut();
    }

    IEnumerator Delete()
    {
        WWWForm form = new WWWForm();
        form.AddField("userId", DBManager.userId.ToString());
        UnityWebRequest www = UnityWebRequest.Post("http://localhost/mds_db/delete.php", form);
        yield return www.SendWebRequest();

        if (www.downloadHandler.text == "0")
        {
            Debug.Log("User Deleted Successfully");
        }
        else
        {
            Debug.Log("Delete Failed. Error #" + www.downloadHandler.text);
        }
    }


}
