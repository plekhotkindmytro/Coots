using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class BackButtonAnimation : MonoBehaviour
{
    private float timePassed = 0;


    private void Update()
    {
        if(isActiveAndEnabled) 
        {
            timePassed += Time.deltaTime;
            if (timePassed >= 5f)
            {
                transform.gameObject.SetActive(false);
                timePassed = 0;
            }
        }
        
        
    }

}
