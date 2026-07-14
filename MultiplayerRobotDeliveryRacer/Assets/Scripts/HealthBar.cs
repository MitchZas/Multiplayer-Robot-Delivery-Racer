using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

   public Slider slider;

   public Gradient gradient;
    public Image fill;

   public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        Debug.Log("SetMaxHealth called with: " + health + " | slider.maxValue is now: " + slider.maxValue);

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        Debug.Log("SetHealth called with: " + health);

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
