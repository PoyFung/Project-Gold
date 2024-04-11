using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineAudio : MonoBehaviour
{
    public Rigidbody rb;

    public AudioSource idle;
    public AudioSource running;
    public AudioSource hit;

    public float minSpeed;
    public float maxSpeed;

    public float minPitch;
    public float maxPitch;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EngineSound();
    }

    public void EngineSound()
    {
        float currentSpeed = rb.velocity.magnitude;
        float pitch = rb.velocity.magnitude / 50f;

        if (currentSpeed < minSpeed)
        {
            idle.Play();
            running.Stop();
        }

        if (currentSpeed > minSpeed && currentSpeed < maxSpeed)
        {
            idle.Stop();
            running.pitch = minPitch + pitch;
        }

        if(currentSpeed > maxSpeed)
        {
            running.pitch = maxPitch;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            hit.Play();
        }
    }
}
