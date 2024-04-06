using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
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
        SceneManager.LoadSceneAsync("Test Room");
    }
}
