using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextAlpha : MonoBehaviour
{

    public Color textColor;
    public Text text;

    void Start()
    {
        text = gameObject.GetComponent<Text>();
        textColor = Color.white;
        textColor.a = 1f;
        text.color = textColor;
   }

    private void Update()
    {
        textColor.a -= .5f * Time.deltaTime;
        text.color = textColor;
    }
}
