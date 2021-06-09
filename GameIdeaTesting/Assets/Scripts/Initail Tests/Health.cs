using System;
using System.Collections;
using System.Collections.Generic;
using Input;
using UnityEngine;

public class Health : MonoBehaviour {
    [SerializeField] private InputReader inputReader;
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    public event Action<float> OnHealthPctChanged = delegate { };

    public void OnEnable() {
        currentHealth = maxHealth;
        inputReader.leftClickEvent += HandleLeftClickEvent;
    }

    public void OnDestroy() {
        inputReader.leftClickEvent -= HandleLeftClickEvent;
    }

    public void ModifyHealth(int amount) {
        currentHealth += amount;

        float currentHealthPct = (float) currentHealth / (float) maxHealth;
        OnHealthPctChanged(currentHealthPct);
    }

    public void HandleLeftClickEvent() {
        if (currentHealth > 10) {
            ModifyHealth(-10);
        }
        else {
            ModifyHealth(-currentHealth);
        }
    }
}