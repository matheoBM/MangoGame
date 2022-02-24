using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MangometerControl : MonoBehaviour
{
    private Slider _slider;

    void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.maxValue = 100;
    }

    /// <summary>
    /// Set new health value on the health bar
    /// </summary>
    /// <param name="health">New health value</param>
    public void SetHealthValue(int health)
    {
        _slider.value = health;
    }
}
