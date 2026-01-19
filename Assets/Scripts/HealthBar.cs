using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider 
        slider;

    private void Awake()
    {
        if(Game._healthBar == null)
        {
            Game._healthBar = this;
        }
    }
    private void Start()
    {
        slider = GetComponent<Slider>();
    }
    public void UpdateHealthbar(float health)
    {
        slider.value = health;
    }
}
