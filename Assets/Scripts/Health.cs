using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;

    private int currentHealth;

    public event Action<float> OnHealthPctChanged = delegate { };

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }
   
    public void ModifyHealth(int amount)
    {
        currentHealth += amount;

        float currentHealthPct = (float)currentHealth / (float)maxHealth;
        OnHealthPctChanged(currentHealthPct);
    }

}
