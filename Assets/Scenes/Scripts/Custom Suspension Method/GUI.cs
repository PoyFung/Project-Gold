using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI speed;
    [SerializeField] TextMeshProUGUI lap;

    public static float currentLap = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        speed.text = "Speed: " + CarPhysics.rbVelocity.ToString("0.00");
        lap.text = "Lap: " + currentLap.ToString() + "/3";
    }
}
