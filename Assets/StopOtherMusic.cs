using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopOtherMusic : MonoBehaviour
{
    
    public AudioSource backgroundMusic;
    private void OnEnable()
    {
        backgroundMusic.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
