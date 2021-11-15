using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image interior;
    [SerializeField] private Slider slider;
    [SerializeField] private Gradient gradient;
    
    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
        
        interior.color = gradient.Evaluate(1f);
    }

    public void SetHealth(float health)
    {
        slider.value = health;
        interior.color = gradient.Evaluate(slider.normalizedValue);
    }

}
