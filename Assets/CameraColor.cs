using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraColor : MonoBehaviour
{
    private static readonly Color32 color1 = new Color32(202, 240, 248, 255);
    private static readonly Color32 color2 = new Color32(253, 252, 220, 255);

    private static readonly Color color = color1;
    void Start()
    {
       /* Camera cam = GetComponent<Camera>();
        cam.backgroundColor = color;*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
