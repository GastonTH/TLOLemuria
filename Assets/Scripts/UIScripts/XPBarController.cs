using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UIScripts
{
    public class XPBarController : MonoBehaviour
    {
        public Slider slider;
        public TextMeshProUGUI lvl;
        public TextMeshProUGUI xp;

        public void setMaxXP(int xp)
        {
            Debug.Log("Setting max xp to " + xp);
            slider.maxValue = xp;
        }
    
        public void setXP(int xp)
        {
            slider.value = xp;
        }
        

        public void updateInfo(int myHeroeLevel, int maxXp, int currentXp)
        {
            lvl.text = myHeroeLevel.ToString();
            xp.text = currentXp+ "/" + maxXp;
            setMaxXP(maxXp);
            setXP(currentXp);
        }
    }
}
