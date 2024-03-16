using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum MenuState
{
    Options,
    TimeTrial
}

public class MenuSelect : MonoBehaviour
{
    public MenuState currentState;
    public void startMenu()
    {
        SceneManager.LoadScene("Start Menu");
    }

    public void hubRoom()
    {
        SceneManager.LoadScene("Hub Room");
    }

    public void testRoom()
    {
        SceneManager.LoadScene("Test Room");
    }
}
