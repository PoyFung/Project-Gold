using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSelect : MonoBehaviour
{
    public void startMenu()
    {
        SceneManager.LoadScene("Start Menu");
    }

    public void testRoom()
    {
        SceneManager.LoadScene("Test Room");
    }
}
