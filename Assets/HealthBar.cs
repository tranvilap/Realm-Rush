using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    [SerializeField]Slider slider = null;
    [SerializeField] Image fill = null;
    [SerializeField] Gradient gradient = null;

    private void Awake()
    {

        SetMaxHealth(100);
        SetHealth(100);
    }

    public void SetMaxHealth(float maxHealth)
    {
        slider.maxValue = maxHealth;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void SetHealth(float value)
    {
        slider.value = value;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
