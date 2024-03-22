using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int Position;

    public Transform FRPos;
    public Transform FLPos;
    public Transform BRPos;
    public Transform BLPos;

    public ObjectList waypointContainer;
    public static List<Transform> waypoints;
    private Vector3 targetPosition;
    public int currentWaypoint;
    public float waypointDistDetection;
    public int passedWaypoints;
    public float distFromWaypoint;

    public GameObject frontRightWheel;
    public GameObject frontLeftWheel;

    private CarPhysics carPhysics;

    public float power=10;
    public float turnAmount=10;

    private float inputVert;
    private float inputHor;

    private void Awake()
    {
        carPhysics=GetComponent<CarPhysics>();
        waypoints = waypointContainer.list;
        currentWaypoint = 0;
        Position = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        //distFromWaypoint = Vector3.Distance(waypoints[currentWaypoint].position, transform.position);
    }

    // Update is called once per frames
    void Update()
    {
        inputVert = Input.GetAxis("Vertical") * power * 10;
        inputHor = Input.GetAxis("Horizontal") * turnAmount*10;
        //distFromWaypoint = Vector3.Distance(waypoints[currentWaypoint].position, transform.position);
        //DistToWaypoint();
    }

    private void FixedUpdate()
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

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Finish Line")
        {
            GUI.currentLap += 1;
        }
    }
}
