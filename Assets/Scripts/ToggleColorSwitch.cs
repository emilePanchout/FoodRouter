using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToggleColorSwitch : MonoBehaviour
{
    public Toggle toggle;
    public Image image;
    public Color onColor;
    public Color offColor;


    public void Start()
    {
        toggle.onValueChanged.AddListener(SwitchColor);



    }
    public void SwitchColor(bool isOn)
    {
        if (isOn)
        {
            image.color = onColor;
        }
        else
        {
            image.color = offColor;
        }
    }
}
