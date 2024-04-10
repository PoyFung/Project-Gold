using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.Windows;

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

    public static float rbVelocity;

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
        Time.timeScale = 1f;
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

        HorizontalFriction(FRPos);
        HorizontalFriction(FLPos);
        HorizontalFriction(BRPos);
        HorizontalFriction(BLPos);

        VerticalFriction(FRPos);
        VerticalFriction(FLPos);
        VerticalFriction(BRPos);
        VerticalFriction(BLPos);

        WheelAnimation(frontRightWheel);
        WheelAnimation(frontLeftWheel);
        WheelAnimation(backRightWheel);
        WheelAnimation(backLeftWheel);

        DebugFunction(FRPos);
        DebugFunction(FLPos);
        DebugFunction(BRPos);
        DebugFunction(BLPos);

        rbVelocity = rb.velocity.magnitude;
    }

    public void Steering(Transform forcePos, GameObject wheel,float input)
    {
        float rotationLimit = input * Time.deltaTime;
        var forceRot = forcePos.localEulerAngles;

        rotationLimit = Mathf.Clamp(rotationLimit,-20,20);
        if (input>0 || input <0)
        {
            forcePos.transform.localRotation = Quaternion.Euler(0, rotationLimit, 0);
        }

        else
        {
            forcePos.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        var rot_y = Quaternion.Euler(-90, forceRot.y+90, 0);
        wheel.transform.localRotation= rot_y;
    }

    public void Acceleration(Transform forcePos,float input)
    {
        if (Physics.Raycast(forcePos.position, -transform.up, out hitInfo, suspensionRestDist, layers))
        {
            Vector3 accelDir = -forcePos.forward;

            //Acceleration
            if (input > 0f || input < 0f)
            {
                float speed = Vector3.Dot(kartFrame.forward, rb.velocity);
                float normSpeed = Mathf.Clamp01(Mathf.Abs(speed) / maxSpeed);
                float torque = powerCurve.Evaluate(normSpeed) * input;
                rb.AddForceAtPosition(accelDir * torque, forcePos.position);
            }
        }
    }

    void WheelAnimation(GameObject wheel)
    {
        float rotationAngle = rb.velocity.magnitude * Time.deltaTime * Mathf.Rad2Deg;
        wheel.transform.Rotate(Vector3.up, rotationAngle);
    }
    void DebugFunction(Transform forcePos)
    {
        Debug.DrawRay(forcePos.position, -forcePos.forward, Color.blue);
        //Debug.DrawRay(forcePos.position, forcePos.up, Color.red);
        Debug.DrawRay(forcePos.position, -forcePos.right, Color.green);
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

            Debug.DrawLine(forcePos.position, hitInfo.point, Color.red);
            wheel.transform.position = hitInfo.point + transform.up;
        }
    }

    void HorizontalFriction(Transform forcePos)
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

    void VerticalFriction(Transform forcePos)
    {
        if (Physics.Raycast(forcePos.position, -transform.up, out hitInfo, suspensionRestDist, layers))
        {
            Vector3 accelDir = -forcePos.forward;
            Vector3 wheelWorldVel = rb.GetPointVelocity(forcePos.position);

            //Friction
            float wheelForwVel = Vector3.Dot(accelDir, wheelWorldVel);
            float velChange = -wheelForwVel * tireGrip;
            float accel = velChange / Time.fixedDeltaTime;

            rb.AddForceAtPosition(accelDir * accel, forcePos.position);
        }
    }
}
