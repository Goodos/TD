using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private int health = 1;

    public void TakeDamage(int damage) { health -= damage; }

    public int GetTowerHp() { return health; }

    public void SetTowerHp(int plusHealth)
    {
        health += plusHealth;
    }
}
