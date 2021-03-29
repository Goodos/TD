using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Data : ScriptableObject
{
    public bool slow = false;

    private int slowdown = 3;
    private int health = 5;
    private int damage = 5;
    private int critDamage = 1;
    private float fireRate = 1;

    public void SetSlowdown()
    {
        slow = true;
        if (slowdown < 6)
        {
            slowdown += 1;
        }
    }

    public int GetSlowdown() { return slowdown; }

    public void SetFireRate()
    {
        if (fireRate > .3f)
        {
            fireRate -= .1f;
        }
    }

    public float GetFireRate() { return fireRate; }

    public void SetDamage()
    {
        damage += 1;
    }

    public int GetDamage() { return damage; }

    public void SetCritDamage()
    {
        critDamage += 1;
    }

    public int GetCritDamage() { return critDamage; }

    public int PlusTowerHealth() { return health; }

    public void SetDefaultParameters()
    {
        slowdown = 3;
        health = 5;
        damage = 5;
        critDamage = 1;
        fireRate = 1;
    }
}