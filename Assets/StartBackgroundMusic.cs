using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBackgroundMusic : MonoBehaviour
{
    public AudioSource backgroundMusic;
    private void OnEnable()
    {
        backgroundMusic.Play();
    }
}
