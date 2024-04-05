using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum MenuState
{
    Hub,
    Mode,
    TimeTrial,
    GrandPrix
}

public class MenuNavigation : MonoBehaviour
{
    public static MenuState currentState;

    public GameObject modeMenu;
    public GameObject grandPrixMenu;
    public GameObject timeTrialMenu;

    private void Awake()
    {
        currentState = MenuState.Hub;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case MenuState.Mode:
                modeMenu.SetActive(true);
                timeTrialMenu.SetActive(false);
                grandPrixMenu.SetActive(false);
                break;

            case MenuState.TimeTrial:
                modeMenu.SetActive(false);
                timeTrialMenu.SetActive(true);
                grandPrixMenu.SetActive(false);
                break;

            case MenuState.GrandPrix:
                modeMenu.SetActive(false);
                timeTrialMenu.SetActive(false);
                grandPrixMenu.SetActive(true);
                break;
        }
    }

    public void OnGrandPrix()
    {
        currentState = MenuState.GrandPrix;
    }

    public void OnTimeTrial()
    {
        currentState = MenuState.TimeTrial;
    }

    public void OnBack()
    {
        currentState = MenuState.Mode;
    }

    public void OnCanadaCup()
    {
        SceneManager.LoadSceneAsync("Canada Cup", LoadSceneMode.Single);
        SceneManager.UnloadSceneAsync("Hub Room");
    }
}
