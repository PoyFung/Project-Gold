using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SphereController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public Rigidbody sphere;

    public GameObject frontRightWheel;
    public GameObject frontLeftWheel;
    public GameObject backRightWheel;
    public GameObject backLeftWheel;

    float inputVert;
    float inputHor;

    public float acceleration = 5f;
    public float maxSpeed;
    public float turnSpeed = 5f;
    public float gravity = 5f;

    public float distFromGround = 5f;

    public LayerMask layerMask;
    RaycastHit hitInfo;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        inputVert = Input.GetAxis("Vertical");
        inputHor = Input.GetAxis("Horizontal");

        player.transform.position=sphere.transform.position-new Vector3(0,distFromGround,0);
    }

    private void FixedUpdate()
    {
        Accelerate();
        if (sphere.velocity.magnitude > 0f || sphere.velocity.magnitude < 0f)
        {
            Turn();
        }
        reactionToGround();
    }

    void Accelerate()
    {
        float speedInput = acceleration * inputVert * 1000;
        sphere.AddForce(player.transform.forward*speedInput);
    }

    void Turn()
    {
        Vector3 turnInput = new Vector3(0f, inputHor * inputVert * turnSpeed * 10 * Time.deltaTime, 0f);
        player.transform.rotation = Quaternion.Euler(player.transform.eulerAngles+turnInput);
    }

    void reactionToGround()
    {
        if (Physics.Raycast(player.transform.position,-player.transform.up, out hitInfo, distFromGround,layerMask))
        {
            Debug.DrawLine(player.transform.position, hitInfo.point, Color.red);
            player.transform.rotation = Quaternion.FromToRotation(player.transform.up, hitInfo.normal) * player.transform.rotation;
        }
    }
}
