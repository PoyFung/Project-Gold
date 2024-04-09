using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI speed;
    [SerializeField] TextMeshProUGUI lap;
    [SerializeField] TextMeshProUGUI position;

    public static int Pos;
    public static bool hasRun = false;

    private PlayerController playerController;

    public static float currentLap = 1;
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        speed.text = "Speed: " + CarPhysics.rbVelocity.ToString("0");
        lap.text = "Lap: " + currentLap.ToString() + "/3";

        if (currentLap == 3 && hasRun == false)
        {
            GameNavigation.OnRaceFinish();
            Standings.GetFinalStandings();
            hasRun = true;
        }
        position.text = "Pos: " + Pos.ToString();
    }
}
