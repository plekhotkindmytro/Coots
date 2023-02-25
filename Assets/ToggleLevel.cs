using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToggleLevel : MonoBehaviour, IPointerClickHandler
{

    public GameObject[] levels;
    public int levelIndex;

    public void OnPointerClick(PointerEventData eventData)
    {
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].SetActive(false);
        }
        levels[levelIndex].SetActive(true);

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
