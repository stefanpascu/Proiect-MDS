using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class DBManager
{
    public static int userId;
    public static string username;
    public static string firstName;
    public static string lastName;
    public static int gamesStarted;
    public static int gamesFinished;

    public static bool LoggedIn 
    { get 
        { 
            return username != null; 
        } 
    }

    public static void LogOut()
    {
        username = null;
        SceneManager.LoadScene(0);
    }

}
