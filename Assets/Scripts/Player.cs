using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] WheelCollider frontRightCol;
    [SerializeField] WheelCollider frontLeftCol;
    [SerializeField] WheelCollider backRightCol;
    [SerializeField] WheelCollider backLeftCol;

    [SerializeField] Transform frontRightTrans;
    [SerializeField] Transform frontLeftTrans;
    [SerializeField] Transform backRightTrans;
    [SerializeField] Transform backLeftTrans;

    public GameObject centerOfMass;
    private Rigidbody rb;

    /*
    public float speed = 3;
    public float turnSpeed = 3;
    */

    public float speed = 500f;
    public float brake = 100f;
    public float turnAngle = 15f;


    private float currentAccel = 0f;
    private float currentBrake = 0f;
    private float currentTurnAngle = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        Vector3 mov = new Vector3(0, 0, -Input.GetAxis("Vertical"));
        Vector3 rot = new Vector3(0, Input.GetAxis("Horizontal"), 0);
        transform.Rotate(rot.normalized*turnSpeed*Time.deltaTime);
        transform.Translate(mov.normalized * speed * Time.deltaTime);
        */
    }

    private void FixedUpdate()
    {
        float inputVert = Input.GetAxis("Vertical");
        float inputHorz = Input.GetAxis("Horizontal");

        if (inputVert<0 || inputVert>0)
        {
            currentAccel = inputVert;
            Accelerate(-inputVert);
        }
        else if (inputVert!=0)
        {
            Accelerate(0);
        }

        //Braking
        if (Input.GetKey(KeyCode.Space))
        {
            currentBrake = brake;
            Braking(currentBrake);
        }
        else
        {
            currentBrake = 0;
            Braking(currentBrake);
        }

        Turning();
        //Wheels
        UpdateWheel(frontLeftCol, frontLeftTrans);
        UpdateWheel(frontRightCol, frontRightTrans);
        UpdateWheel(backLeftCol, backLeftTrans);
        UpdateWheel(backRightCol, backRightTrans);
    }

    void Accelerate(float input)
    {
        Debug.Log("ACCELRATING");
        frontRightCol.motorTorque = speed * input;
        frontLeftCol.motorTorque = speed * input;
        backRightCol.motorTorque = speed * input;
        backLeftCol.motorTorque = speed * input;
    }

    void Braking(float input)
    {
        Debug.Log("BRAKING");
        frontRightCol.brakeTorque =input;
        frontLeftCol.brakeTorque = input;
        backRightCol.brakeTorque = input;
        backLeftCol.brakeTorque = input;
    }

    void Turning()
    {
        currentTurnAngle = turnAngle * Input.GetAxis("Horizontal");
        frontLeftCol.steerAngle = currentTurnAngle;
        frontRightCol.steerAngle = currentTurnAngle;
    }

    void UpdateWheel(WheelCollider col, Transform trans)
    {
        Vector3 position;
        Quaternion rotation;
        col.GetWorldPose(out position, out rotation);

        Vector3 angles = rotation.eulerAngles;
        rotation=Quaternion.Euler(angles.x, angles.y, angles.z);

        trans.position = position;
        trans.rotation = rotation;
    }
}
