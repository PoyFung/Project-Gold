using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public enum GameState
{
    GUI,
    Pause,
    Finish,
}
public class GameNavigation : MonoBehaviour
{
    public ObjectList Positions;
    public List<Transform> RacerResults;
    public Stopwatch timer = new Stopwatch();
    public static GameState currentState;

    public bool hasRun = false;

    public GameObject GUI;
    public GameObject pause;
    public GameObject results;

    private void Awake()
    {
        RacerResults = Positions.list;
        currentState = GameState.GUI;
    }

    // Start is called before the first frame update
    void Start()
    {
        
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
        if (hasRun == false)
        {
            for (int i = 0; i < Standings.finalStands.Count; i++)
            {
                TextMeshProUGUI pos = RacerResults[i].transform.Find("Position Num").GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI name = RacerResults[i].transform.Find("Racer Name").GetComponent<TextMeshProUGUI>();
                //TextMeshProUGUI points = RacerResults[i].transform.Find("Points").GetComponent<TextMeshProUGUI>();

                pos.text = Standings.finalStands[i].position.ToString();
                name.text = Standings.finalStands[i].name;
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
            for (int i = 0; i < Standings.finalStands.Count; i++)
            {
                RacerResults[i].gameObject.SetActive(true);
            }
        }
    }
}
