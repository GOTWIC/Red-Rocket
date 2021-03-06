using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public delegate void Fade();
    public static event Fade FadeIn;
    public static event Fade FadeOut;

    int sceneNum;

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
        sceneNum = 1;
        LoadScene();
    }

    void LoadCurrentScene_E()
    {
        sceneNum = SceneManager.GetActiveScene().buildIndex;
        LoadScene();
    }

    void LoadNextScene_E()
    {
        sceneNum = SceneManager.GetActiveScene().buildIndex + 1;
        LoadScene();
    }

    void Play_E()
    {
        sceneNum = SceneManager.GetActiveScene().buildIndex + 1;
        LoadScene();
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

    void LoadScene()
    {
        //SceneManager.LoadScene(sceneNum);
        StartCoroutine(SceneLoading());
    }

    IEnumerator SceneLoading()
    {
        FadeOut();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneNum);
        FadeIn();
    }



}
