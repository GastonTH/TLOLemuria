using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionBarController : MonoBehaviour
{
    public Slider slider;

    public void setMaxActionTime(int actionTime)
    {
        slider.maxValue = actionTime;
        slider.value = actionTime;
    }
    
    public void setActionTime(int actionTime)
    {
        slider.value = actionTime;
    }
}