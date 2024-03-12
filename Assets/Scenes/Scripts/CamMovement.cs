using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CamMovement : MonoBehaviour
{
    public Transform frontFace;
    public Transform backFace;
    public float camSpeed=1.0f;
    //private Vector3 offset=new Vector3 (0,5,-5);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 camPos = backFace.position;
        transform.position = Vector3.Lerp(transform.position, camPos, camSpeed * Time.deltaTime);
        transform.LookAt(frontFace.position);
    }
}
