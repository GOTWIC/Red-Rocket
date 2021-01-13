using UnityEngine;
using UnityEngine.UI;
public class RedRocketLogoScript  : MonoBehaviour
{
    [SerializeField] RawImage rawImg;
    [SerializeField] float transformSpeed = 300f;
    [SerializeField] float transformTime = .35f;
    [SerializeField] float waitTime = 2f;

    public bool transformEnabled = false;
    public float transitionTime;

    public byte alpha = 100;

    public delegate void CreateMenu();
    public static event CreateMenu StartMenuEnabled;

    void Start()
    {
        Invoke("Transform",waitTime);
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

    }

    private void ChangeTransparency()
    {
        Color color;
        color = new Color32(0, 0, 0, alpha);
        if (rawImg) rawImg.color = new Color(rawImg.color.r, rawImg.color.g, rawImg.color.b, color.a);
    }

    
}