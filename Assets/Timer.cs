using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{

    public float maxTimeInSeconds = 60f;
    public GameController gameController;
    public TMP_Text timerText;
    private float timePassed = 0;
    private bool canCount = false;

    private void OnEnable()
    {
        timePassed = 0;
        timerText.text = "";

    }
    private void OnDisable()
    {
        timePassed = 0;
        timerText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if(!canCount && gameController.GetCanClick())
        {
            canCount = true;
        }
        if(canCount)
        {
            timePassed += Time.deltaTime;
            if (timePassed >= maxTimeInSeconds)
            {
                timerText.text = "Time left: \n0 seconds";
                gameController.ShowRestartLevelPopup();
            }
            else
            {
                timerText.text = "Time left: \n" + TimeLeftInIntSeconds() + " seconds";
            }
        }
       
    }

    private int TimeLeftInIntSeconds()
    {
        return (int)(maxTimeInSeconds - timePassed);
    }
}
