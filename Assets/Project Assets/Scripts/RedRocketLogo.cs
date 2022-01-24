using UnityEngine;
using UnityEngine.UI;
public class RedRocketLogo  : MonoBehaviour
{
    [SerializeField] RawImage rawImg;
    [SerializeField] float transformSpeed = 70f;
    [SerializeField] float transformTime = 1.5f;
    [SerializeField] float startTime = 3f;
    float waitTime;

    public bool transformEnabled = false;
    public float transitionTime;

    float transparency = 0;

    public byte alpha = 100;

    public delegate void CreateMenu();
    public static event CreateMenu StartMenuEnabled;

    bool logoEnable = false;
    bool logoDisable = false;

    void Start()
    {
        waitTime = startTime + 6;
        Invoke("Transform",waitTime);
        MakeTransparent();
        Invoke("MakeVisible", startTime);
    }

    private void Transform()
    {
        transformEnabled = true;
        StartMenuEnabled();
    }


    void Update()
    {
        float transformScaler = -1 * ((float)Screen.width) / 715 * Time.deltaTime * transformSpeed;

        transitionTime = Time.fixedTime - waitTime;
        transformTime = 105f / (float)transformSpeed ;

        if (transformEnabled)
        {
            rawImg.transform.Translate(transformScaler, 0, 0);
        }

        if(transitionTime >= transformTime)
        {
            transformEnabled = false;
        }

        if(transparency >= 1)
        {
            logoEnable = false;
        }

        if(logoEnable == true)
        {
            transparency += .005f * 100 * Time.deltaTime;
            if (rawImg) rawImg.color = new Color(rawImg.color.r, rawImg.color.g, rawImg.color.b, transparency);
        }

        if(logoDisable)
        {
            transparency -= .02f * 100 * Time.deltaTime; ;
            if (rawImg) rawImg.color = new Color(rawImg.color.r, rawImg.color.g, rawImg.color.b, transparency);
        }

        if(transparency == 0)
        {
            logoDisable = false;
        }

    }

    private void MakeTransparent()
    {
        if (rawImg) rawImg.color = new Color(rawImg.color.r, rawImg.color.g, rawImg.color.b, 0);
    }

    private void MakeVisible()
    {
        logoEnable = true;
    }

    private void MakeInvisible()
    {
        logoDisable = true;
    }

    private void OnEnable()
    {
        StartMenuOptions.Play_E += MakeInvisible;
    }

    private void OnDisable()
    {
        StartMenuOptions.Play_E -= MakeInvisible;
    }


}