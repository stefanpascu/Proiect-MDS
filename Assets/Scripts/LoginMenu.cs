using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LoginMenu : MonoBehaviour
{
    public void GoToRegister()
    {
        SceneManager.LoadScene(1);
    }

}
