using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody player;
    public float power=10;
    public float turnAmount=10;

    public static float inputVert;
    public static float inputHor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frames
    void Update()
    {
        inputVert = Input.GetAxis("Vertical")*power;
        inputHor = Input.GetAxis("Horizontal")*turnAmount*10;
    }

    private void FixedUpdate()
    {
        //Accelerate();
        //Rotate();
    }

    void Accelerate()
    {
        player.AddForce(player.transform.forward*inputVert, ForceMode.Force);
    }

    void Rotate()
    {
        float rotationAmount = inputHor * Time.deltaTime * turnAmount;
        Quaternion deltaRotation = Quaternion.Euler(0f, rotationAmount, 0f);

        Quaternion targetRotation = player.rotation * deltaRotation;
        player.MoveRotation(Quaternion.Slerp(player.rotation, targetRotation, 0.1f));
    }
}
