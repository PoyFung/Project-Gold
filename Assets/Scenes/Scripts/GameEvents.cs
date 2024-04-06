using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using TMPro;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countDown;

    //public static GameEvents current = null;
    //public event Action onFinishRace;

    public static List<Rigidbody> kartListRB;
    public float timeLeft = 4;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        kartListRB = new List<Rigidbody>();
        for (int i = 0; i < Standings.kartList.Count; i++)
        {
            Rigidbody currentRB = null;
            kart currentKart = Standings.kartList[i];
            if (currentKart.playerKart != null)
            {
                currentRB = currentKart.playerKart.GetComponent<Rigidbody>();
            }

            else if (currentKart.cpuKart != null)
            {
                currentRB = currentKart.cpuKart.GetComponent<Rigidbody>();
            }

            if (currentRB != null)
            {
                Freeze(currentRB);
                kartListRB.Add(currentRB);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft>=1)
        {
            countDown.text = (timeLeft).ToString("0");
        }

        if (timeLeft <= 0.5)
        {
            countDown.text = "GO";
            foreach (var rb in kartListRB)
            {
                UnFreeze(rb);
            }
        }

        if (timeLeft < 0)
        {
            countDown.gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        
    }

    public void Freeze(Rigidbody rb)
    {
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX;
    }

    public void UnFreeze(Rigidbody rb)
    {
        rb.constraints = RigidbodyConstraints.None;
    }
}
