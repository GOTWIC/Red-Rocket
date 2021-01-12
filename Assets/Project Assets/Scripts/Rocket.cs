using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Security.Cryptography;
//using System.Numerics;


public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 200f;
    [SerializeField] float Thrust = 500f;

    [SerializeField] AudioClip MainEngine;
    [SerializeField] AudioClip Death;
    [SerializeField] AudioClip Chime;

    [SerializeField] ParticleSystem MainEngineFlames;
    [SerializeField] ParticleSystem SuccessParticles;
    [SerializeField] ParticleSystem DeathEffect;

    [SerializeField] bool collisionDetection = true;

    int sceneNum;



    public Renderer rend;
    Rigidbody rigidbody;
    AudioSource audioSource;
    public Animator transition;

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
        Restart();
        RespondToThrusting();
        RespondToRotating();
    }

    private void Restart()
    {
        if (Input.GetKey(KeyCode.R))
        {
            Invoke("LoadFirstScene", 0f);
        }
    }



    //Rocket Movement, Audio, and Effects
    private void RespondToThrusting()
    {
        float thrustForce = (Time.deltaTime * Thrust);
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow))
        {
            Thrusting(thrustForce);
        }

        else if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.UpArrow))
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
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.RotateAround(center, Vector3.forward, rotationspeed * Time.deltaTime);
        }

        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
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
        if (state != State.alive || !collisionDetection)
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
        //StartCoroutine(SceneLoader(1));
        SceneManager.LoadScene(1);
    }

    private void LoadNextScene()
    { 
        sceneNum = SceneManager.GetActiveScene().buildIndex + 1;
        //StartCoroutine(SceneLoader(sceneNum));

        SceneManager.LoadScene(sceneNum);
        state = State.alive;
    }

    private void LoadCurrentScene()
    {
        sceneNum = SceneManager.GetActiveScene().buildIndex;
        //StartCoroutine(SceneLoader(sceneNum));
        SceneManager.LoadScene(sceneNum);
        state = State.alive;
    }


    /*
    IEnumerator SceneLoader(int sceneNum)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneNum);
        state = State.alive;
    }

    */


}
