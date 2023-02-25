using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private static readonly string SIZE_KEY = "size";
    private static readonly string BLURED_KEY = "blured";
    private static readonly string COLOR_KEY = "color";
    private static readonly string SELECTED = "selected";

    private static readonly Color BLUE = new Color32(62, 120, 178, 255);
    private static readonly Color RED = new Color32(236, 11, 67, 255);
    private static readonly Color GREEN = new Color32(19, 138, 54, 255);


    public Slider sizeSlider;

    public Button redButton;
    public Button greenButton;
    public Button blueButton;

    public Button solidButton;
    public Button bluredButton;

    public void Quit()
    {
        Application.Quit();
    }
    public void SetSize()
    {
        float value = sizeSlider.value;
        PlayerPrefs.SetFloat(SIZE_KEY, value);
    }

    public void SetBlured()
    {
        Debug.Log("SetBlured");
        Debug.Log("Has Blured: " + PlayerPrefs.HasKey(BLURED_KEY));

        PlayerPrefs.SetString(BLURED_KEY, "blured");
        bluredButton.GetComponentInChildren<TMPro.TMP_Text>().text = SELECTED;
        solidButton.GetComponentInChildren<TMPro.TMP_Text>().text = string.Empty;
    }

    public void SetSolid()
    {
        PlayerPrefs.DeleteKey(BLURED_KEY);
        Debug.Log("SetSolid");
        Debug.Log("Has Blured: "+ PlayerPrefs.HasKey(BLURED_KEY));
        bluredButton.GetComponentInChildren<TMPro.TMP_Text>().text = string.Empty;
        solidButton.GetComponentInChildren<TMPro.TMP_Text>().text = SELECTED;
    }

    public void SetRedColor()
    {
        PlayerPrefs.SetString(COLOR_KEY, "red");
        redButton.GetComponentInChildren<TMPro.TMP_Text>().text = SELECTED;
        greenButton.GetComponentInChildren<TMPro.TMP_Text>().text = string.Empty;
        blueButton.GetComponentInChildren<TMPro.TMP_Text>().text = string.Empty;
        sizeSlider.fillRect.GetComponent<Image>().color = RED; 
    }

    public void SetBlueColor()
    {
        PlayerPrefs.SetString(COLOR_KEY, "blue");
        redButton.GetComponentInChildren<TMPro.TMP_Text>().text = string.Empty;
        greenButton.GetComponentInChildren<TMPro.TMP_Text>().text = string.Empty;
        blueButton.GetComponentInChildren<TMPro.TMP_Text>().text = SELECTED;

        sizeSlider.fillRect.GetComponent<Image>().color = BLUE;
    }

    public void SetGreenColor()
    {
        PlayerPrefs.SetString(COLOR_KEY, "green");
        redButton.GetComponentInChildren<TMPro.TMP_Text>().text = string.Empty;
        greenButton.GetComponentInChildren<TMPro.TMP_Text>().text = SELECTED;
        blueButton.GetComponentInChildren<TMPro.TMP_Text>().text = string.Empty;

        sizeSlider.fillRect.GetComponent<Image>().color = GREEN;
    }



}
