using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class CarPhysics : MonoBehaviour
{
    public Rigidbody rb;

    public Transform kartFrame;
    public Transform FRPos;
    public Transform FLPos;
    public Transform BRPos;
    public Transform BLPos;

    public float suspensionRestDist=1;
    public float springStrength=100;
    public float springDamper=15;

    [Range(0, 1)] public float tireGrip;

    public float maxSpeed = 10;
    public AnimationCurve powerCurve;

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
        Suspension(FRPos, frontRightWheel);
        Suspension(FLPos, frontLeftWheel);
        Suspension(BRPos, backRightWheel);
        Suspension(BLPos, backLeftWheel);

        SideFriction(FRPos, frontRightWheel);
        SideFriction(FLPos, frontLeftWheel);
        SideFriction(BRPos, backRightWheel);
        SideFriction(BLPos, backLeftWheel);

        Acceleration(FRPos, frontRightWheel);
        Acceleration(FLPos, frontLeftWheel);

        Steering(FRPos,frontRightWheel);
        Steering(FLPos,frontLeftWheel);

        DebugFunction(FRPos);
        DebugFunction(FLPos);
        DebugFunction(BRPos);
        DebugFunction(BLPos);
    }

    void Steering(Transform forcePos, GameObject wheel)
    {
        float input = PlayerController.inputHor;
        float rotationAmount = input * Time.deltaTime;
        var forceRot = forcePos.eulerAngles;

        rotationAmount = Mathf.Clamp(rotationAmount,-100,100);
        if (input>0 || input <0)
        {
            forcePos.transform.localRotation=Quaternion.Euler(0, rotationAmount, 0);
        }

        var rot_y = Quaternion.Euler(-90, forceRot.y+90, 0);
        wheel.transform.rotation=rot_y;
    }

    void WheelAnimation()
    {

    }

    void Suspension(Transform forcePos,GameObject wheel)
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

    void SideFriction(Transform forcePos, GameObject wheel)
    {
        if (Physics.Raycast(forcePos.position, -transform.up, out hitInfo, suspensionRestDist, layers))
        {
            Vector3 frictionDir = forcePos.right;
            Vector3 wheelWorldVel = rb.GetPointVelocity(forcePos.position);

            float wheelHorzVel = Vector3.Dot(frictionDir, wheelWorldVel);
            float velChange = -wheelHorzVel * tireGrip;
            float accel = velChange / Time.fixedDeltaTime;

            rb.AddForceAtPosition(frictionDir * accel*10, forcePos.position);
        }
    }

    void Acceleration(Transform forcePos, GameObject wheel)
    {
        float input = PlayerController.inputVert;
        if (Physics.Raycast(forcePos.position, -transform.up, out hitInfo, suspensionRestDist, layers))
        {
            Vector3 accelDir = -forcePos.forward;
            Vector3 wheelWorldVel = rb.GetPointVelocity(forcePos.position);

            //Friction
            float wheelForwVel = Vector3.Dot(accelDir, wheelWorldVel);
            float velChange = -wheelForwVel * tireGrip;
            float accel = velChange / Time.fixedDeltaTime;

            rb.AddForceAtPosition(accelDir * accel, forcePos.position);

            //Acceleration
            if (input>0f || input<0f)
            {
                float speed = Vector3.Dot(kartFrame.forward, rb.velocity);
                float normSpeed = Mathf.Clamp01(Mathf.Abs(speed) / maxSpeed);
                float torque = powerCurve.Evaluate(normSpeed) * input;
                rb.AddForceAtPosition(accelDir * torque, forcePos.position);
            }
        }
    }

    void DebugFunction(Transform forcePos)
    {
        Debug.DrawLine(forcePos.position, new Vector3(forcePos.rotation.x+forcePos.position.x+1, forcePos.position.y, forcePos.position.z), Color.red);
        Debug.DrawLine(forcePos.position, new Vector3(forcePos.position.x, forcePos.position.y+1, forcePos.position.z), Color.green);
        Debug.DrawLine(forcePos.position, new Vector3(forcePos.position.x, forcePos.position.y, forcePos.position.z + 1), Color.blue);
    }
}
