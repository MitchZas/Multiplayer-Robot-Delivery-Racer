using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

   public Slider slider;

   public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        Debug.Log("SetMaxHealth called with: " + health + " | slider.maxValue is now: " + slider.maxValue);

    }

    public void SetHealth(int health)
    {
        slider.value = health;
        Debug.Log("SetHealth called with: " + health);
    }
}
