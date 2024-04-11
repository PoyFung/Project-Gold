using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GUI : MonoBehaviour
{
    public GameObject positionObject;
    public GameObject timeObject;

    [SerializeField] TextMeshProUGUI speed;
    [SerializeField] TextMeshProUGUI lap;
    [SerializeField] TextMeshProUGUI position;
    [SerializeField] TextMeshProUGUI time;

    public static int Pos;
    public bool hasRun = false;

    private PlayerController playerController;

    public static float currentLap = 1;
    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log(HubNavigation.currentState);
    }

    // Update is called once per frame
    void Update()
    {
        speed.text = "Speed: " + CarPhysics.rbVelocity.ToString("0");
        lap.text = "Lap: " + currentLap.ToString() + "/2";
        
        if (HubNavigation.currentState == HubState.GrandPrix || HubNavigation.currentState == HubState.Hub)
        {
            positionObject.SetActive(true);
            position.text = "Pos: " + Pos.ToString();
        }

        else if (HubNavigation.currentState == HubState.TimeTrial || HubNavigation.currentState == HubState.Hub)
        {
            timeObject.SetActive(true);

            int minutes= Mathf.FloorToInt(GameEvents.timePassed / 60f);
            int seconds = Mathf.FloorToInt(GameEvents.timePassed % 60f);

            time.text = "Time: " + minutes.ToString("00") + ":" + seconds.ToString("00");
        }

        if (currentLap == 3 && hasRun == false)
        {
            GameNavigation.OnRaceFinish();
            Standings.GetFinalStandings();
            GameEvents.timer.Stop();
            SaveVars.addTime(GameEvents.timePassed);
            hasRun = true;
        }
    }
}
