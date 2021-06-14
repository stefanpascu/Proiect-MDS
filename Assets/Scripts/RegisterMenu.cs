using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;


public class RegisterMenu : MonoBehaviour
{
    public InputField firstnameField;
    public InputField lastnameField;
    public InputField usernameField;
    public InputField passwordField;
    public InputField repasswordField;
    public Button submitButton;

    public void GoToLogin()
    {
        SceneManager.LoadScene(0);
    }

    public void CallRegister()
    {
        StartCoroutine(Register());
    }

    IEnumerator Register()
    {
        WWWForm form = new WWWForm();
        form.AddField("firstname", firstnameField.text);
        form.AddField("lastname", lastnameField.text);
        form.AddField("username", usernameField.text);
        form.AddField("pwd", passwordField.text);
        form.AddField("repwd", repasswordField.text);
        //Debug.Log(firstnameField.text);
        UnityWebRequest www = UnityWebRequest.Post("http://localhost/mds_db/register.php", form);
        yield return www.SendWebRequest();

        if (www.downloadHandler.text == "0")
        {
            Debug.Log("User Created Successfully");
            SceneManager.LoadScene(0);
        }
        else
        {
            Debug.Log("User Creation Failed. Error #" + www.downloadHandler.text);
        }

    }
}
