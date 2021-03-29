using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;
    private Vector3 target;
    private float speed;

    void Update()
    {
        if (transform.position.y <= -5f)
        {
            Destroy(gameObject);
        }
        SimpleCannonBall();
    }

    public void SetParameters(Vector3 setTarget, float setSpeed, int setDamage)
    {
        target = setTarget;
        speed = setSpeed;
        damage = setDamage;
        target = (target - transform.position).normalized;
    }

    void SimpleCannonBall()
    {
        transform.Translate(target * speed * Time.deltaTime);
    }    
}
