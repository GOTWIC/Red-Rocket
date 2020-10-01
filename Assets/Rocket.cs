using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
//using System.Numerics;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 200f;
    [SerializeField] float Thrust = 200f;
    Rigidbody rigidbody;
    AudioSource audioSource;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        Thrusting();
        Rotating();
    }

    private void Thrusting()
    {
        float thrustForce = (Time.deltaTime * Thrust);
        if (Input.GetKey(KeyCode.W))
        {
            rigidbody.AddRelativeForce(Vector3.up * thrustForce * 10 );

            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }


            if (audioSource.volume < 1f)
            {
                audioSource.volume += 0.015f;
            }

        }

        else if (!Input.GetKey(KeyCode.W))
        {
            if(audioSource.volume > .1f)
            {
                audioSource.volume -= 0.015f;
            }

            
            if(audioSource.volume == 0)
            {
                audioSource.Stop();
            }
            
        }
    }

    private void Rotating()
    {
        rigidbody.freezeRotation = true;
        
        float rotationspeed = (Time.deltaTime * rcsThrust);
        if (Input.GetKey(KeyCode.A))
        {       
            transform.Rotate(Vector3.forward*rotationspeed);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward*rotationspeed);
        }

        rigidbody.freezeRotation = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Friendly")
        { 
            print("OK");
        }

        else
            print("DEAD");
    }


}
