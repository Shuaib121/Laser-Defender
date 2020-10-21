using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSingleton : MonoBehaviour
{
    // Using singleton to play music throughout all scenes without stopping
    void Awake()
    {
        int musicCheck = FindObjectsOfType<MusicSingleton>().Length;
        if(musicCheck > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
