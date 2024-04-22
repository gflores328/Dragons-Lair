using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
    public MouseFollower mouseFollower;
    public Slider redSlider;
    public Slider greenSlider;
    public Slider blueSlider;

    public Image spriteRenderer;

    private void Start()
    {
        // Subscribe to slider value changed events
        redSlider.onValueChanged.AddListener(OnColorChanged);
        greenSlider.onValueChanged.AddListener(OnColorChanged);
        blueSlider.onValueChanged.AddListener(OnColorChanged);
    }

    // Method called when any of the sliders value changes
    void OnColorChanged(float value)
    {
        // Get the color values from sliders
        Color newColor = new Color(redSlider.value, greenSlider.value, blueSlider.value);
        // Set the color of the cursor
        mouseFollower.SetColor(newColor);
        spriteRenderer.color = newColor;
    }
}
