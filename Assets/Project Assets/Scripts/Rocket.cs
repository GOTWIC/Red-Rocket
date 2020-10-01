using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
//using System.Numerics;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 150f;
    [SerializeField] float Thrust = 200f;
    Rigidbody rigidbody;
    AudioSource audioSource;

    enum State {alive, dying, transcending}
    State state = State.alive;

    void Start()
    {
        Application.targetFrameRate = 300;
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        state = State.alive;
    }



    void Update()
    {
       if(state == State.alive)
            ProcessInput();
        else if (state != State.alive)
            audioSource.Stop();
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
                audioSource.Play();


            if (audioSource.volume < 1f)
                audioSource.volume += 0.015f;
        }

        else if (!Input.GetKey(KeyCode.W))
        {
            if(audioSource.volume > .1f)
                audioSource.volume -= 0.015f;

            if(audioSource.volume == 0)
                audioSource.Stop();
        }
    }

    private void Rotating()
    {
        rigidbody.freezeRotation = true;
        Collider collider;
        Vector3 center;

        collider = GetComponent<Collider>();
        center = collider.bounds.center;

        float rotationspeed = (Time.deltaTime * rcsThrust * 40);
        if (Input.GetKey(KeyCode.A))
        {
            transform.RotateAround(center, Vector3.forward, rotationspeed * Time.deltaTime);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            transform.RotateAround(center, Vector3.back, rotationspeed * Time.deltaTime);
        }

        rigidbody.freezeRotation = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (state != State.alive)
            return;


        if(collision.gameObject.tag == "Friendly")
            return; 

        else if (collision.gameObject.tag == "Finish")
        {
            state = State.transcending;
            Invoke("LoadNextScene", 1f);
        }

        else
        {
            state = State.dying;
            Invoke("LoadFirstScene", 1f);
        }
            
    }

    private void LoadFirstScene()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(1);
        state = State.alive;
    }
}
