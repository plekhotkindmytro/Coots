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

    public GameObject restartLevelPopup;
    public GameObject completeLevelPopup;

    private bool canClick = false;
    private bool levelComplete = false;




    public void Quit()
    {
        Application.Quit();
    }

    public void LoadCurrentLevel()
    {
        levelComplete = false;
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

    public void ShowRestartLevelPopup()
    {
        if (levelComplete)
        {
            return;
        }



        SetCanClick(false);


        restartLevelPopup.SetActive(true);



    }

    public void ShowCompleteLevelPopup()
    {
        int nextLevel = 0;
        if (PlayerPrefs.HasKey(CURRENT_LEVEL_KEY))
        {
            int currentLevel = PlayerPrefs.GetInt(CURRENT_LEVEL_KEY);
            nextLevel = currentLevel + 1;
        }
        else
        {
            nextLevel++;
        }

        SetCanClick(false);
        if (nextLevel >= levels.Length)
        {
            SceneManager.LoadScene(1);
        }
        else
        {

            completeLevelPopup.SetActive(true);
        }
    }

    public void LoadNextLevel()
    {
        levelComplete = false;
        int nextLevel = 0;
        if (PlayerPrefs.HasKey(CURRENT_LEVEL_KEY))
        {
            int currentLevel = PlayerPrefs.GetInt(CURRENT_LEVEL_KEY);
            nextLevel = currentLevel + 1;

        }
        else
        {
            nextLevel++;
        }

        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].SetActive(false);
        }
        if (nextLevel < levels.Length)
        {
            levels[nextLevel].SetActive(true);
            PlayerPrefs.SetInt(CURRENT_LEVEL_KEY, nextLevel);

        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }

    public void Restart()
    {
        levelComplete = false;
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }

    public void SetCanClick(bool value)
    {
        canClick = value;
    }

    public bool GetCanClick()
    {
        return canClick;
    }

    internal void CompleteLevel()
    {
        levelComplete = true;
    }
}
