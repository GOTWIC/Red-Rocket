using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceLights : MonoBehaviour
{

    [SerializeField] Light blueLight;
    [SerializeField] Light redLight;

    bool changeBlueIntensity = false;
    bool changeRedIntensity = false;
    void Start()
    {
        blueLight.intensity = 0f;
        redLight.intensity = 0f;
        Invoke("EnableLight", 1.5f);
    }

    void Update()
    {
        if (changeBlueIntensity)
        {
            blueLight.intensity += .01f * Time.deltaTime * 150;
        }

        if (changeRedIntensity)
        {
            redLight.intensity += .012f * Time.deltaTime * 150;
        }

        if (blueLight.intensity >= 6f)
        {
            changeBlueIntensity = false;
        }

        if (redLight.intensity >= 5f)
        {
            changeRedIntensity = false;
        }
    }

    void EnableLight()
    {
        changeBlueIntensity = true;
        changeRedIntensity = true;
    }
}
