using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody player;
    public float power=10;
    public float turnAmount=10;

    public static float getInputVert;
    public static float getInputHor;

    public static float inputVert;
    public static float inputHor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frames
    void Update()
    {
        inputVert = getInputVert*power*10;
        inputHor = getInputHor*turnAmount*10;
    }

    private void FixedUpdate()
    {
        
    }
}
