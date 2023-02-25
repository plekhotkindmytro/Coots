using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private static readonly string CURRENT_LEVEL_KEY = "currentLevel";
    public GameObject[] levels;

    public void Quit()
    {
        Application.Quit();
    }

    public void LoadCurrentLevel()
    {
        int currentLevel = 0;
        if (PlayerPrefs.HasKey(CURRENT_LEVEL_KEY))
        {
            currentLevel = PlayerPrefs.GetInt(CURRENT_LEVEL_KEY);
        }
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].SetActive(false);
        }

        levels[currentLevel].SetActive(true);
    }

    public void LoadNextLevel()
    {
        int nextLevel = 0;
        if (PlayerPrefs.HasKey(CURRENT_LEVEL_KEY))
        {
            int currentLevel = PlayerPrefs.GetInt(CURRENT_LEVEL_KEY);
            nextLevel = currentLevel + 1;

        } else
        {
            nextLevel++;
        }

        

        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].SetActive(false);
        }
        if(nextLevel < levels.Length)
        {
            levels[nextLevel].SetActive(true);
            PlayerPrefs.SetInt(CURRENT_LEVEL_KEY, nextLevel);
            
        } else
        {
            SceneManager.LoadScene(1);
        }
    }

    public void Restart()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }
}
