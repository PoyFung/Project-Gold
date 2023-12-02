using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Suspension : MonoBehaviour
{
    public Rigidbody rb;

    public Transform FRForce;
    public Transform FLForce;
    public Transform BRForce;
    public Transform BLForce;

    public GameObject frontRightWheel;
    public GameObject frontLeftWheel;
    public GameObject backRightWheel;
    public GameObject backLeftWheel;

    public float suspensionDist=5f;
    public float oldDist;
    public float suspensionPower = 100f;
    public float dampSensitivity = 500f;
    public float maxDamp = 50f;

    public LayerMask layers;

    private GameObject force;

    Ray rayPos1;
    RaycastHit hitInfo;

    // Start is called before the first frame update
    private void Awake()
    {
        oldDist = suspensionDist;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        SuspensionFunction(FRForce, frontRightWheel);
        SuspensionFunction(FLForce, frontLeftWheel);
        SuspensionFunction(BRForce, backRightWheel);
        SuspensionFunction(BLForce, backLeftWheel);
    }

    void SuspensionFunction(Transform force,GameObject wheel)
    {
        if (Physics.Raycast(force.position,-transform.up, out hitInfo, suspensionDist, layers))
        {
            Vector3 suspensionForce = Mathf.Clamp(suspensionDist - hitInfo.distance, 0, 3) * suspensionPower * transform.up;
            Vector3 dampeningForce = Mathf.Clamp((oldDist - hitInfo.distance) * dampSensitivity, 0, maxDamp) * transform.up;
            rb.AddForceAtPosition(suspensionForce+dampeningForce*Time.deltaTime, hitInfo.point);

            Debug.DrawLine(force.position, wheel.transform.position, Color.red);
            wheel.transform.position = hitInfo.point+transform.up;
        }
        oldDist = hitInfo.distance;
    }
}
