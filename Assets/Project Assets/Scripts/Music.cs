using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    void Awake()
    {
        GameObject [] musics = GameObject.FindGameObjectsWithTag("Music");
        
        
        if (musics.Length > 1)
            Destroy(this.gameObject);
       

        DontDestroyOnLoad(this.gameObject);
    }
}
