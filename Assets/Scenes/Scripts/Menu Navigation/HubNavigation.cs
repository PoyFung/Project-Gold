using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum HubState
{
    Hub,
    Mode,
    TimeTrial,TrialHistory,
    GrandPrix
}

public class HubNavigation : MonoBehaviour
{
    public static HubState currentState;
    public bool hasRun1 = false;
    public bool hasRun2 = false;    

    public GameObject modeMenu;
    public GameObject grandPrixMenu;
    public GameObject timeTrialMenu;
    public GameObject candaCupMenu;

    public ObjectList Times;
    public List<Transform> TrialResults;

    private void Awake()
    {
        currentState = HubState.Hub;
    }

    // Start is called before the first frame update
    void Start()
    {
        TrialResults = Times.list;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasRun2 == false)
        {
            trialResults();
        }
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

            case HubState.TrialHistory:
                timeTrialMenu.SetActive(false);
                candaCupMenu.SetActive(true);
                if (hasRun1 == false)
                {
                    for (int i = 0; i < SaveVars.savedTimes.Count; i++)
                    {
                        TrialResults[i].gameObject.SetActive(true);
                    }
                    hasRun1 = true;
                }
                break;

            case HubState.GrandPrix:
                modeMenu.SetActive(false);
                timeTrialMenu.SetActive(false);
                grandPrixMenu.SetActive(true);
                break;
        }
    }

    public void trialResults()
    {
        for (int i = 0; i < SaveVars.savedTimes.Count; i++)
        {
            TextMeshProUGUI pos = TrialResults[i].transform.Find("Position Num").GetComponent<TextMeshProUGUI>();
            //TextMeshProUGUI name = RacerResults[i].transform.Find("Racer Name").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI time = TrialResults[i].transform.Find("Times").GetComponent<TextMeshProUGUI>();

            pos.text = (i + 1).ToString();
            int minutes = Mathf.FloorToInt(SaveVars.savedTimes[i] / 60f);
            int seconds = Mathf.FloorToInt(SaveVars.savedTimes[i] % 60f);

            time.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        }
        hasRun2 = true;
    }

    public void OnGrandPrix()
    {
        currentState = HubState.GrandPrix;
    }

    public void OnTimeTrial()
    {
        currentState = HubState.TimeTrial;
    }

    public void OnTrialHistory()
    {
        currentState = HubState.TrialHistory;
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
