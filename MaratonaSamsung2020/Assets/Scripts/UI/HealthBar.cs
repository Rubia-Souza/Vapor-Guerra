using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    public Slider healthSlider;
    public FloatEvent healthChangendEvent;

    public HealthData healthData;

    void Start() {
        healthSlider.maxValue = healthData.MaxHealth;
        UpdateHealthBarSlider(healthData.MaxHealth);
        healthChangendEvent.RegisterCallback(UpdateHealthBarSlider);
    }

    public void UpdateHealthBarSlider(float currentHealth) {
        healthSlider.value = currentHealth;
    }

}
