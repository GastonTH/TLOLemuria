using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBarController : MonoBehaviour
{
    public Slider slider;

    public void setMaxMana(int mana, int currentMana)
    {
        Debug.Log("Setting max mana to " + mana);
        slider.maxValue = mana;
        slider.value = currentMana;
    }
    
    public void setMana(int mana)
    {
        slider.value = mana;
    }
}
