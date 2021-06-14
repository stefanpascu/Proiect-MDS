using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;


public class LoginMenu : MonoBehaviour
{
    public InputField usernameField;
    public InputField passwordField;
    public Button submitButton;

    public void GoToRegister()
    {
        SceneManager.LoadScene(1);
    }

    public void CallLogin()
    {
        StartCoroutine(Login());
    }

    IEnumerator Login()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", usernameField.text);
        form.AddField("pwd", passwordField.text);
        //Debug.Log(passwordField.text);
        UnityWebRequest www = UnityWebRequest.Post("http://localhost/mds_db/login.php", form);
        yield return www.SendWebRequest();

        if (www.downloadHandler.text[0] == '0')
        {
            DBManager.userId = int.Parse(www.downloadHandler.text.Split('\t')[1]);
            DBManager.firstName = www.downloadHandler.text.Split('\t')[2];
            DBManager.lastName = www.downloadHandler.text.Split('\t')[3];
            DBManager.username = www.downloadHandler.text.Split('\t')[4];
            DBManager.gamesStarted = int.Parse(www.downloadHandler.text.Split('\t')[5]);
            DBManager.gamesFinished = int.Parse(www.downloadHandler.text.Split('\t')[6]);
            Debug.Log("Logged In Successfully");
            SceneManager.LoadScene(2);
        }
        else
        {
            Debug.Log("Log in Failed. Error #" + www.downloadHandler.text);
        }

    }

}
