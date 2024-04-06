using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CPU : MonoBehaviour
{
    public int Position;

    public Transform FRPos;
    public Transform FLPos;
    public Transform BRPos;
    public Transform BLPos;

    public GameObject frontRightWheel;
    public GameObject frontLeftWheel;

    public ObjectList waypointContainer;
    public static List<Transform> waypoints;
    public int currentWaypoint;

    private CarPhysics carPhysics;

    private Vector3 targetPosition;

    public float waypointDistDetection;
    public int passedWaypoints;
    public float distFromWaypoint;

    public float power = 10;
    public float turnAmount = 10;

    private float inputVert;
    private float inputHor;

    private void Awake()
    {
        carPhysics = GetComponent<CarPhysics>();
        waypoints = waypointContainer.list;
        currentWaypoint = 0;
    }

    private void Start()
    {
        distFromWaypoint = Vector3.Distance(waypoints[currentWaypoint].position, transform.position);
    }

    void Update()
    {
        DistToWaypoint();
        distFromWaypoint = Vector3.Distance(waypoints[currentWaypoint].position,transform.position);

        float forwardAmount = 0f;
        float turnDir = 0f;

        Vector3 dirToMovePosition = (targetPosition - transform.position).normalized;
        float dot = Vector3.Dot(transform.forward, dirToMovePosition);

        forwardAmount = 1f;

        float angleToDir = Vector3.SignedAngle(transform.forward, dirToMovePosition, Vector3.up);

        if (angleToDir>0)
        {
            turnDir = -1f;
        }

        else
        {
            turnDir = 1f;
        }

        inputVert = forwardAmount * power * 10;
        inputHor = turnDir * turnAmount * 10;

    }

    public void FixedUpdate()
    {
        carPhysics.Steering(FRPos, frontRightWheel, inputHor);
        carPhysics.Steering(FLPos, frontLeftWheel, inputHor);

        carPhysics.Acceleration(FRPos, inputVert);
        carPhysics.Acceleration(FLPos, inputVert);
        carPhysics.Acceleration(BRPos, inputVert);
        carPhysics.Acceleration(BLPos, inputVert);
    }

    public void DistToWaypoint()
    {
        SetTargetPosition(waypoints[currentWaypoint].position);
        if (Vector3.Distance(waypoints[currentWaypoint].position, transform.position) < waypointDistDetection)
        {
            currentWaypoint++;
            passedWaypoints++;
            if (currentWaypoint == waypoints.Count)
            {
                currentWaypoint = 0;
            }
        }
        Debug.DrawRay(transform.position, waypoints[currentWaypoint].position - transform.position, Color.yellow);
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }
}
