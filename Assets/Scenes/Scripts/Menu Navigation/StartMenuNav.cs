using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum StartState
{
    Start,
    Settings
}

public class StartMenuNav : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject settingsMenu;

    public static StartState currentState;

    private void Awake()
    {
        currentState = StartState.Start;
    }

    public void Update()
    {
        switch (currentState)
        {
            case StartState.Start:
                startMenu.SetActive(true);
                settingsMenu.SetActive(false);
                break;

            case StartState.Settings:
                settingsMenu.SetActive(true);
                startMenu.SetActive(false);
                break;
        }
    }

    public void OnSettings()
    {
        currentState = StartState.Settings;
    }
    public void OnBack()
    {
        currentState = StartState.Start;
    }

    public void hubRoom()
    {
        SceneManager.LoadScene("Hub Room");
    }

    public void testRoom()
    {
        SceneManager.LoadSceneAsync("Test Room");
    }

    public void OnQuit()
    {
        EditorApplication.ExitPlaymode();
        Application.Quit();
    }
}
