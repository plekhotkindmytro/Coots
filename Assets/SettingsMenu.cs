using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
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


    private void Start()
    {
        Configure();
    }
    void OnEnable()
    {
        Configure();
    }

    private void Configure()
    {
        SetSizeSliderValue();
        SetSelectedColor();
        SetSelectedShape();
    }

    private void SetSizeSliderValue()
    {
        if (PlayerPrefs.HasKey(SIZE_KEY))
        {
            float size = PlayerPrefs.GetFloat(SIZE_KEY);
            sizeSlider.value = size;
        }
    }

    private void SetSelectedColor()
    {
        if (PlayerPrefs.HasKey(COLOR_KEY))
        {
            string colorName = PlayerPrefs.GetString(COLOR_KEY);

            if ("red".Equals(colorName))
            {
                redButton.GetComponentInChildren<TMPro.TMP_Text>().text = SELECTED;
                greenButton.GetComponentInChildren<TMPro.TMP_Text>().text = string.Empty;
                blueButton.GetComponentInChildren<TMPro.TMP_Text>().text = string.Empty;
                sizeSlider.fillRect.GetComponent<Image>().color = RED;
            }
            else if ("green".Equals(colorName))
            {
                redButton.GetComponentInChildren<TMPro.TMP_Text>().text = string.Empty;
                greenButton.GetComponentInChildren<TMPro.TMP_Text>().text = SELECTED;
                blueButton.GetComponentInChildren<TMPro.TMP_Text>().text = string.Empty;
                sizeSlider.fillRect.GetComponent<Image>().color = GREEN;
            }
            else if ("blue".Equals(colorName))
            {
                redButton.GetComponentInChildren<TMPro.TMP_Text>().text = string.Empty;
                greenButton.GetComponentInChildren<TMPro.TMP_Text>().text = string.Empty;
                blueButton.GetComponentInChildren<TMPro.TMP_Text>().text = SELECTED;
                sizeSlider.fillRect.GetComponent<Image>().color = BLUE;
            }
        }
    }

    private void SetSelectedShape()
    {
        if (PlayerPrefs.HasKey(BLURED_KEY))
        {
            bluredButton.GetComponentInChildren<TMPro.TMP_Text>().text = SELECTED;
            solidButton.GetComponentInChildren<TMPro.TMP_Text>().text = string.Empty;
        }
        else
        {
            bluredButton.GetComponentInChildren<TMPro.TMP_Text>().text = string.Empty;
            solidButton.GetComponentInChildren<TMPro.TMP_Text>().text = SELECTED;
        }
    }

}
