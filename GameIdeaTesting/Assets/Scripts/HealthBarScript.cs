using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    private Image healthBar;
    public float CurrentHealth;
    private float maxHealth;
    public PlayerData player;

    private void Start()
    {
        // Suchen der Healthbar
        healthBar = GetComponent<Image>();
        maxHealth = player.getMaxHealth();
    }

    // Update is called once per frame
    void Update()
    {
        CurrentHealth = player.getHealth();
        healthBar.fillAmount = CurrentHealth / maxHealth;
    }
}
