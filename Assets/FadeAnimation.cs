using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FadeAnimation : MonoBehaviour
{

    [SerializeField] RawImage rawImg;
    float transparency = 0;

    bool enableFadeIn = false, enableFadeOut = false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (rawImg) rawImg.color = new Color(rawImg.color.r, rawImg.color.g, rawImg.color.b, 0);
        //Invoke("FadeOut",4f);
    }

    void OnEnable()
    {
        SceneLoader.FadeOut += FadeOut;
        SceneLoader.FadeIn += FadeIn;
    }


    void OnDisable()
    {
        SceneLoader.FadeOut -= FadeOut;
        SceneLoader.FadeIn -= FadeIn;
    }

    void Update()
    {
        if(enableFadeOut)
        {
            transparency += .02f * 100 * Time.deltaTime;
            if (rawImg) rawImg.color = new Color(rawImg.color.r, rawImg.color.g, rawImg.color.b, transparency);
            if (transparency >= 1)
                enableFadeOut = false;
        }

        if(enableFadeIn)
        {
            transparency -= .02f * 100 * Time.deltaTime;
            if (rawImg) rawImg.color = new Color(rawImg.color.r, rawImg.color.g, rawImg.color.b, transparency);
            if (transparency <= 0)
                enableFadeIn = false;
        }
    }

    void FadeOut()
    {
        enableFadeOut = true;
    }

    void FadeIn()
    {
        enableFadeIn = true;
    }

}
