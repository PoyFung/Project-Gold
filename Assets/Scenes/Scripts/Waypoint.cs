using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public List<Transform> waypoints;

    private void Awake()
    {
        foreach (Transform tr in gameObject.GetComponentsInChildren<Transform>())
        {
            waypoints.Add(tr);
        }
        waypoints.Remove(waypoints[0]);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
