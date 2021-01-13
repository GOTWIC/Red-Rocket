using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuOptions : MonoBehaviour
{
    //Names ending with "M" signify methods to disambiguate from events (ending with "E") with the same name.

    [SerializeField] CanvasGroup canvasGroup;

    public delegate void MenuFunctionality();
    public static event MenuFunctionality Play_E;
    public static event MenuFunctionality Help_E;
    public static event MenuFunctionality Quit_E;

    public bool fadeEnabled = false;

    void Start()
    {
        canvasGroup.alpha = 0f;
    }

    void OnEnable()
    {
        RedRocketLogoScript.StartMenuEnabled += FadeIn;
    }


    void OnDisable()
    {
        RedRocketLogoScript.StartMenuEnabled -= FadeIn;
    }

    private void FadeIn()
    {
        Invoke("EnableFadeIn", .1f);
    }

    private void EnableFadeIn()
    {
        fadeEnabled = true;
    }

    void Update()
    {
        if(fadeEnabled)
        {
            canvasGroup.alpha += .7f * Time.deltaTime ;
        }
    }

    public void Play_M()
    {
        Play_E();
    }

    public void Help_M()
    {
        Help_E();
    }

    public void Quit_M()
    {
        Quit_E();
    }

}
