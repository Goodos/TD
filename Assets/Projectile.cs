using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;
    Vector3 target;
    float speed;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= 2f)
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
        //Debug.Log("shoot");
        //Vector3 direction = (target - transform.position).normalized;
        transform.Translate(target * speed * Time.deltaTime);// = projectile.transform.position + direction * speed * Time.deltaTime;
    }    
}
