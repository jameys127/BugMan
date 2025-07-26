using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider slider;
    void Awake()
    {
        slider = GetComponent<Slider>();
    }
    public void SetFullHealth(int health){
        slider.maxValue = health;
        slider.value = health;
    }
    public void DamageHealth(int damage){
        slider.value -= damage;
    }
    public void HealHealth(int heal){
        slider.value += heal;
    }
}
