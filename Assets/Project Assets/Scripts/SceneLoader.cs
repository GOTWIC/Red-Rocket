using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    void Start()
    {
        //Invoke("LoadFirstSceneE", 10f);
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        Rocket.LoadFirstScene_E += LoadFirstScene_E;
        Rocket.LoadCurrentScene_E += LoadCurrentScene_E;
        Rocket.LoadNextScene_E += LoadNextScene_E;
        StartMenuOptions.Play_E += Play_E;
        StartMenuOptions.Help_E += Help_E;
        StartMenuOptions.Quit_E += Quit_E;
    }


    void OnDisable()
    {
        Rocket.LoadFirstScene_E -= LoadFirstScene_E;
        Rocket.LoadCurrentScene_E -= LoadCurrentScene_E;
        Rocket.LoadNextScene_E -= LoadNextScene_E;
        StartMenuOptions.Play_E -= Play_E;
        StartMenuOptions.Help_E -= Help_E;
        StartMenuOptions.Quit_E -= Quit_E;
    }

    void LoadFirstScene_E()
    {
        var sceneNum = 1;
        SceneManager.LoadScene(sceneNum);
    }

    void LoadCurrentScene_E()
    {
        var sceneNum = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneNum);
    }

    void LoadNextScene_E()
    {
        var sceneNum = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(sceneNum);
    }

    void Play_E()
    {
        var sceneNum = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(sceneNum);
    }

    void Help_E()
    {
        //var sceneNum = SceneManager.GetActiveScene().buildIndex + 1;
        //SceneManager.LoadScene(sceneNum);
        ;
    }

    void Quit_E()
    {
        Application.Quit();
    }








}
