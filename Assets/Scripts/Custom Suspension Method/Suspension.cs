using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Suspension : MonoBehaviour
{
    public Rigidbody rb;

    public Transform FRPos;
    public Transform FLPos;
    public Transform BRPos;
    public Transform BLPos;

    public float suspensionRestDist=1;
    public float springStrength=100;
    public float springDamper=15;
    [Range(0, 1)] public float tireGrip;

    public GameObject frontRightWheel;
    public GameObject frontLeftWheel;
    public GameObject backRightWheel;
    public GameObject backLeftWheel;

    public LayerMask layers;
    RaycastHit hitInfo;

    // Start is called before the first frame update
    private void Awake()
    {

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
        SuspensionFunction(FRPos, frontRightWheel);
        SuspensionFunction(FLPos, frontLeftWheel);
        SuspensionFunction(BRPos, backRightWheel);
        SuspensionFunction(BLPos, backLeftWheel);

        SideFrictionFunction(FRPos, frontRightWheel);
        SideFrictionFunction(FLPos, frontLeftWheel);
        SideFrictionFunction(BRPos, backRightWheel);
        SideFrictionFunction(BLPos, backLeftWheel);
    }

    void SuspensionFunction(Transform forcePos,GameObject wheel)
    {
        if (Physics.Raycast(forcePos.position,-transform.up, out hitInfo, suspensionRestDist, layers))
        {
            Vector3 springForceDir = forcePos.up;
            Vector3 wheelWorldVel = rb.GetPointVelocity(forcePos.position);

            float offset = suspensionRestDist-hitInfo.distance;
            float wheelVertVel = Vector3.Dot(springForceDir,wheelWorldVel);
            float totalForce = (offset*springStrength)-(wheelVertVel*springDamper);

            rb.AddForceAtPosition(springForceDir*totalForce, forcePos.position);

            Debug.DrawLine(forcePos.position, wheel.transform.position, Color.red);
            wheel.transform.position = hitInfo.point + transform.up;
        }
    }

    void SideFrictionFunction(Transform forcePos, GameObject wheel)
    {
        Vector3 frictionDir = forcePos.right;
        Vector3 wheelWorldVel = rb.GetPointVelocity(forcePos.position);

        float wheelHorzVel = Vector3.Dot(frictionDir, wheelWorldVel);
        float velChange = -wheelHorzVel * tireGrip;
        float accel = velChange / Time.fixedDeltaTime;

        rb.AddForceAtPosition(frictionDir * accel, forcePos.position);
    }

    void AccelrateFunction()
    {

    }
}
