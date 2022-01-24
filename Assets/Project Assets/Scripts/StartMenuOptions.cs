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


    public bool menuEnable = false;
    public bool menuDisable = false;

    void Start()
    {
        canvasGroup.alpha = 0f;
    }

    void OnEnable()
    {
        RedRocketLogo.StartMenuEnabled += FadeIn;
    }


    void OnDisable()
    {
        RedRocketLogo.StartMenuEnabled -= FadeIn;
    }

    private void FadeIn()
    {
        Invoke("EnableFadeIn", .4f);
    }

    private void EnableFadeIn()
    {
        menuEnable = true;
    }

    void Update()
    {
        if(menuEnable)
        {
            canvasGroup.alpha += .005f * Time.deltaTime * 100 ;
        }

        if (canvasGroup.alpha == 1)
        {
            menuEnable = false;
        }

        if (menuDisable)
        {
            canvasGroup.alpha -= .02f * 100 * Time.deltaTime; ;
        }

        if (canvasGroup.alpha == 0)
        {
            menuDisable = false;
        }
    }

    public void Play_M()
    {
        Play_E();
        menuDisable = true;
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
