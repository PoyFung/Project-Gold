using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.MemoryProfiler;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public enum GameState
{
    GUI,
    Pause,
    Settings,
    Finish,
}
public class GameNavigation : MonoBehaviour
{
    public ObjectList Positions;
    public ObjectList Times;
    public List<Transform> RacerResults;
    public static List<Transform> TrialResults;
    public Stopwatch timer = new Stopwatch();
    public static GameState currentState;

    public bool hasRun = false;

    public GameObject GUI;
    public GameObject MiniMap;
    public GameObject pause;
    public GameObject settings;
    public GameObject results;

    private void Awake()
    {
        currentState = GameState.GUI;
    }

    // Start is called before the first frame update
    void Start()
    {
        TrialResults = Times.list;
        RacerResults = Positions.list;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            currentState = GameState.Pause;
        }

        switch (currentState)
        {
            case GameState.GUI:
                Time.timeScale = 1;
                GUI.SetActive(true);
                pause.SetActive(false);
                break;

            case GameState.Pause:
                Time.timeScale = 0;
                AudioListener.volume = 0;
                GUI.SetActive(false);
                MiniMap.SetActive(false);
                pause.SetActive(true);
                break;

            case GameState.Settings:
                pause.SetActive(false);
                settings.SetActive(true);
                break;

            case GameState.Finish:
                GUI.SetActive(false);
                MiniMap.SetActive(false);
                resultScreenTransitions();
                break;
        }
    }

    public void OnQuit()
    {
        SceneManager.LoadScene("Hub Room");
    }

    public void OnResume()
    {
        AudioListener.volume = 1;
        currentState = GameState.GUI;
    }

    public void OnSettings()
    {
        currentState = GameState.Settings;
    }

    public static void OnRaceFinish()
    {
        currentState = GameState.Finish;
    }

    public void resultScreenTransitions()
    {
        UnityEngine.Debug.Log(HubNavigation.currentState);
        if (hasRun == false && HubNavigation.currentState == HubState.GrandPrix)
        {
            for (int i = 0; i < Standings.finalStands.Count; i++)
            {
                TextMeshProUGUI pos = RacerResults[i].transform.Find("Position Num").GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI name = RacerResults[i].transform.Find("Racer Name").GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI points = RacerResults[i].transform.Find("Points").GetComponent<TextMeshProUGUI>();

                pos.text = Standings.finalStands[i].position.ToString();
                name.text = Standings.finalStands[i].name;
                points.text = Standings.finalStands[i].points.ToString();
            }
            hasRun = true;
        }

        else if (hasRun == false && HubNavigation.currentState == HubState.TrialHistory)
        {
            //UnityEngine.Debug.Log(SaveVars.savedTimes.Count);
            for (int i = 0; i < SaveVars.savedTimes.Count; i++)
            {
                TextMeshProUGUI pos = TrialResults[i].transform.Find("Position Num").GetComponent<TextMeshProUGUI>();
                //TextMeshProUGUI name = RacerResults[i].transform.Find("Racer Name").GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI time = TrialResults[i].transform.Find("Times").GetComponent<TextMeshProUGUI>();

                pos.text = (i+1).ToString();
                int minutes = Mathf.FloorToInt(SaveVars.savedTimes[i] / 60f);
                int seconds = Mathf.FloorToInt(SaveVars.savedTimes[i] % 60f);

                time.text = minutes.ToString("00") + ":" + seconds.ToString("00");
            }
            hasRun = true;
        }

        timer.Start();
        Transform finishText = results.transform.Find("Finish");
        Transform panel = results.transform.Find("Panel");

        finishText.gameObject.SetActive(true);
        if (timer.Elapsed.TotalSeconds >= 3)
        {
            finishText.gameObject.SetActive(false);
            panel.gameObject.SetActive(true);
            if (HubNavigation.currentState == HubState.GrandPrix)
            {
                for (int i = 0; i < Standings.finalStands.Count; i++)
                {
                    RacerResults[i].gameObject.SetActive(true);
                }
            }

            else if (HubNavigation.currentState == HubState.TrialHistory)
            {
                for (int i = 0; i < SaveVars.savedTimes.Count; i++)
                {
                    TrialResults[i].gameObject.SetActive(true);
                }
            }
        }
    }
}
