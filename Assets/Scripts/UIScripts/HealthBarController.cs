using UnityEngine;
using UnityEngine.UI;

namespace UIScripts
{
    public class HealthBarController : MonoBehaviour
    {

        public Slider slider;

        public void setMaxHealth(int health, int currentHealth)
        {
            //Debug.Log("Setting max health to " + health);
            slider.maxValue = health;
            slider.value = currentHealth;
        }
    
        public void setHealth(int health)
        {
            slider.value = health;
        }
    
    }
}
