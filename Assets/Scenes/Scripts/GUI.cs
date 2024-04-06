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

    private PlayerController playerController;

    public static float currentLap = 0;
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        speed.text = "Speed: " + CarPhysics.rbVelocity.ToString("0.00");
        if (currentLap == 0)
        {
            lap.text = "Lap: 1/3";
        }

        else
        {
            lap.text = "Lap: " + currentLap.ToString() + "/3";
        }

        if (currentLap == 2)
        {
            GameNavigation.OnRaceFinish();
            Standings.GetFinalStandings();
        }
        position.text = "Pos: " + Pos.ToString();
    }
}
