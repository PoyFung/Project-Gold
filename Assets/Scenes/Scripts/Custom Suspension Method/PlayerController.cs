using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform FRPos;
    public Transform FLPos;
    public Transform BRPos;
    public Transform BLPos;

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
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frames
    void Update()
    {
        inputVert = Input.GetAxis("Vertical") * power * 10;
        inputHor = Input.GetAxis("Horizontal") * turnAmount*10;
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
}
