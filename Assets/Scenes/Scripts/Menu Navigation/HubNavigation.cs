using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum HubState
{
    Hub,
    Mode,
    TimeTrial,
    GrandPrix
}

public class HubNavigation : MonoBehaviour
{
    public static HubState currentState;

    public GameObject modeMenu;
    public GameObject grandPrixMenu;
    public GameObject timeTrialMenu;

    private void Awake()
    {
        currentState = HubState.Hub;
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
            case HubState.Mode:
                modeMenu.SetActive(true);
                timeTrialMenu.SetActive(false);
                grandPrixMenu.SetActive(false);
                break;

            case HubState.TimeTrial:
                modeMenu.SetActive(false);
                timeTrialMenu.SetActive(true);
                grandPrixMenu.SetActive(false);
                break;

            case HubState.GrandPrix:
                modeMenu.SetActive(false);
                timeTrialMenu.SetActive(false);
                grandPrixMenu.SetActive(true);
                break;
        }
    }

    public void OnGrandPrix()
    {
        currentState = HubState.GrandPrix;
    }

    public void OnTimeTrial()
    {
        currentState = HubState.TimeTrial;
    }

    public void OnBack()
    {
        currentState = HubState.Mode;
    }

    public void OnCanadaCup()
    {
        SceneManager.LoadScene("Canada Cup");
    }
}
