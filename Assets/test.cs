using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
//using System.Numerics;

public class test : MonoBehaviour
{

    Rigidbody rigidbody;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
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
        if (Input.GetKey(KeyCode.W))
        {
            rigidbody.AddRelativeForce(Vector3.up);
            if (!audioSource.isPlaying)
                audioSource.Play();
        }

        else if (!Input.GetKey(KeyCode.W))
        {
            audioSource.Stop();
        }
    }

    private void Rotating()
    {
        rigidbody.freezeRotation = true;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * 150);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * Time.deltaTime * 150);
        }

        rigidbody.freezeRotation = false;
    }


}
