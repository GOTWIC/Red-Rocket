using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Security.Cryptography;
//using System.Numerics;
  

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 150f;
    [SerializeField] float Thrust = 200f;

    [SerializeField] AudioClip MainEngine;
    [SerializeField] AudioClip Death;
    [SerializeField] AudioClip Chime;

    [SerializeField] ParticleSystem MainEngineFlames;
    [SerializeField] ParticleSystem SuccessParticles;
    [SerializeField] ParticleSystem DeathEffect;

    int sceneNum;



    public Renderer rend;
    Rigidbody rigidbody;
    AudioSource audioSource;

    enum State {alive, dying, transcending}
    State state = State.alive;



    //General Functions
    void Start()
    {
        Application.targetFrameRate = 300;
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        state = State.alive;
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        //LoadFirstScene();
    }

    void Update()
    {
        if (state == State.alive)
            ProcessInput();
       
        else
        {
            if (MainEngineFlames.isEmitting == true)
                MainEngineFlames.Stop();
        }
    }

    private void ProcessInput()
    {
        RespondToThrusting();
        RespondToRotating();
    }



    //Rocket Movement, Audio, and Effects
    private void RespondToThrusting()
    {
        float thrustForce = (Time.deltaTime * Thrust);
        if (Input.GetKey(KeyCode.W))
        {
            Thrusting(thrustForce);
        }

        else if (!Input.GetKey(KeyCode.W))
        {
            NoThrusting();
        }
    }

    private void RespondToRotating()
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

    private void Thrusting(float thrustForce)
    {
        rigidbody.AddRelativeForce(Vector3.up * thrustForce * 10);

        if (!audioSource.isPlaying)
            audioSource.PlayOneShot(MainEngine);

        if (audioSource.volume < 1f)
            audioSource.volume += 0.02f;

        MainEngineFlames.Play();
    }

    private void NoThrusting()
    {
         if (audioSource.volume > 0f)
             audioSource.volume -= 0.02f;

         if (audioSource.volume == 0)
             audioSource.Stop();

        MainEngineFlames.Stop();
    }



    //Collision

    private void OnCollisionEnter(Collision collision)
    {
        if (state != State.alive)
            return;

        if(collision.gameObject.tag == "Friendly")
            return; 

        else if (collision.gameObject.tag == "Finish")
            StartSuccessSequence();

        else
            StartDeathSequence();
    }



    //Transitions
    private  void StartDeathSequence()
    {
        state = State.dying;
        audioSource.Stop();
        audioSource.PlayOneShot(Death);
        audioSource.volume = .1f;
        DeathEffect.Play();
        Destroy();
        Invoke("LoadCurrentScene", 1.75f);
    }

    private void StartSuccessSequence()
    {
        state = State.transcending;
        audioSource.Stop();
        audioSource.PlayOneShot(Chime);
        audioSource.volume = .1f;
        SuccessParticles.Play();
        Destroy();
        Invoke("LoadNextScene", 2f);
    }

    private void Destroy()
    {
        //Destroy(gameObject);
        rend.enabled = false;
    }




    //Load Scenes
    private void LoadFirstScene()  
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextScene()
    { 
        sceneNum = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneNum+1);
        state = State.alive;
    }

    private void LoadCurrentScene()
    {
        sceneNum = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneNum);
        state = State.alive;
    }
}
