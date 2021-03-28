﻿using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Health healthModify;
    private GameController gc;
    private bool movement = true;
    private Vector3 direction;
    private Data data;
    private int startSpeed;

    public int health;
    public int damage;
    public int speed;

    void Start()
    {
        data = Resources.Load<Data>("Data");
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        healthModify = GetComponent<Health>();
        direction = (new Vector3(0, 1.5f, 0) - transform.position).normalized;
        //startSpeed = speed;
    }

    void Update()
    {
        if (health <= 0)
        {
            gc.enemyCounter--;
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        StopCoroutine(DoDamage(null));
    }

    private void FixedUpdate()
    {
        if (movement)
        {
            Move();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Tower")
        {
            movement = false;
            StartCoroutine(DoDamage(collision.gameObject));
        }
    }

    private void OnTriggerEnter(Collider trigger)
    {
        if (trigger.gameObject.tag == "Projectile")
        {
            TakeDamage(trigger.gameObject.GetComponent<Projectile>().damage);
            if (data.slow)
            {
                if (startSpeed == speed)
                {
                    StartCoroutine(Slow());
                }
            }
        }
    }

    public void SetParameters(int setHealth, int setDamage, int setSpeed)
    {
        health = setHealth;
        damage = setDamage;
        speed = setSpeed;
        startSpeed = speed;
    }

    IEnumerator DoDamage(GameObject tower)
    {
        while (true)
        {
            tower.GetComponent<Tower>().TakeDamage(damage);
            yield return new WaitForSeconds(1f);
        }
    }

    void TakeDamage(int damage)
    {
        healthModify.ModifyHealth(-damage);
        health -= damage;
    }

    IEnumerator Slow()
    {
        speed -= 3;
        float timer = 1;
        timer -= Time.deltaTime;
        Debug.Log(timer);
        if (timer <= 0)
        {
            speed += 3;
            yield break;
        }
        //yield return null;
    }

    void Move()
    {
        transform.position = transform.position + direction * speed * Time.deltaTime;
        //rb.MovePosition(transform.position + direction * speed * Time.fixedDeltaTime);
    }
}
