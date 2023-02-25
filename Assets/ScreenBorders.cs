using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBorders : MonoBehaviour
{

    public GameObject borderPrefab;
    private float currentPosX;
    private float currentPosY;
    private static readonly float scale = 1f;
    private static readonly float halfScale = scale / 2f;


    private GameObject topBorder;
    private GameObject bottomBorder;
    private GameObject leftBorder;
    private GameObject rightBorder;

    void Start()
    {
        topBorder = Instantiate(borderPrefab);
        rightBorder = Instantiate(borderPrefab);
        bottomBorder = Instantiate(borderPrefab);
        leftBorder = Instantiate(borderPrefab);
       
        currentPosX = Camera.main.orthographicSize * Camera.main.aspect + halfScale;
        currentPosY = Camera.main.orthographicSize + halfScale;
        UpdateBorders();


    }

    private void FixBordersForWebBrowserIfResized()
    {
        float posX = Camera.main.orthographicSize * Camera.main.aspect + halfScale;
        float posY = Camera.main.orthographicSize + halfScale;

        if (posX != currentPosX || posY != currentPosY)
        {
            UpdateBorders();
            currentPosX = posX;
            currentPosY = posY;
        }
    }

    // Update is called once per frame
    void Update()
    {
        FixBordersForWebBrowserIfResized();
    }

    private void UpdateBorders()
    {
        float stretchedScaleX = Camera.main.orthographicSize * 2 * Camera.main.aspect;
        float stretchedScaleY = Camera.main.orthographicSize * 2;
       
        topBorder.transform.localPosition = new Vector2(0, currentPosY);
        topBorder.transform.localScale = new Vector2(stretchedScaleX, scale);
        
        bottomBorder.transform.localPosition = new Vector2(0, -currentPosY);
        bottomBorder.transform.localScale = new Vector2(stretchedScaleX, scale);
        
        leftBorder.transform.localPosition = new Vector2(-currentPosX, 0);
        leftBorder.transform.localScale = new Vector2(scale, stretchedScaleY);
        
        rightBorder.transform.localPosition = new Vector2(currentPosX, 0);
        rightBorder.transform.localScale = new Vector2(scale, stretchedScaleY);
    }
}
