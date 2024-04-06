using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public enum GameState
{
    GUI,
    Pause,
    Finish
}

public class RacerResult
{
    TextMeshProUGUI pos;
    TextMeshProUGUI name;
    TextMeshProUGUI points;
}
public class GameNavigation : MonoBehaviour
{
    public ObjectList PositionRacerResults;
    public List<Transform> RacerResults;
    public Stopwatch timer = new Stopwatch();
    public static GameState currentState;

    public GameObject GUI;
    public GameObject pause;
    public GameObject results;

    private void Awake()
    {
        currentState = GameState.GUI;
    }

    // Start is called before the first frame update
    void Start()
    {
        RacerResults = PositionRacerResults.list;
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
                GUI.SetActive(false);
                pause.SetActive(true);
                break;

            case GameState.Finish:
                GUI.SetActive(false);
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
        currentState = GameState.GUI;
    }

    public static void OnRaceFinish()
    {
        currentState = GameState.Finish;
    }

    public void resultScreenTransitions()
    {
        for (int i=0; i < Standings.finalStands.Count; i++)
        {
            
        }

        timer.Start();
        Transform finishText = results.transform.Find("Finish");
        Transform panel = results.transform.Find("Panel");

        finishText.gameObject.SetActive(true);
        if (timer.Elapsed.TotalSeconds >= 3)
        {
            finishText.gameObject.SetActive(false);

        }
    }
}
