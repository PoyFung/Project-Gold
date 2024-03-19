using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPU : MonoBehaviour
{
    //[SerializeField] private Transform targetPosTransform;
    public Waypoint waypointContainer;
    public List<Transform> waypoints;
    public int currentWaypoint;

    private PlayerController controller;
    private Vector3 targetPosition;

    public float waypointDist;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
    }

    private void Start()
    {
        waypoints=waypointContainer.waypoints;
        currentWaypoint=0;
    }

    private void Update()
    {
        SetTargetPosition(waypoints[currentWaypoint].position);
        if (Vector3.Distance(waypoints[currentWaypoint].position, transform.position)<waypointDist)
        {
            currentWaypoint++;
            if(currentWaypoint==waypoints.Count)
            {
                currentWaypoint = 0;
            }
        }
        Debug.DrawRay(transform.position, waypoints[currentWaypoint].position-transform.position,Color.yellow);
        float forwardAmount = 0f;
        float turnAmount = 0f;

        Vector3 dirToMovePosition = (targetPosition - transform.position).normalized;
        float dot = Vector3.Dot(transform.forward, dirToMovePosition);

        Debug.Log(dot);
        forwardAmount = 1f;

        float angleToDir = Vector3.SignedAngle(transform.forward, dirToMovePosition, Vector3.up);
        Debug.Log(angleToDir);

        if (angleToDir>0)
        {
            turnAmount = -1f;
        }

        else
        {
            turnAmount = 1f;
        }

        PlayerController.getInputVert = forwardAmount;
        PlayerController.getInputHor = turnAmount;

    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }
}
