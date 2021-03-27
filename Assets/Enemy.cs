using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Health healthModify;
    public int health;
    public int damage;
    public int speed;
    GameController gc;
    bool movement = true;
    Vector3 direction;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        healthModify = GetComponent<Health>();
        direction = (new Vector3(0, 1.5f, 0) - transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            gc.enemyCounter--;
            Destroy(gameObject);
        }
        //Debug.Log("do");
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
        }
    }

    public void SetParameters(int setHealth, int setDamage, int setSpeed)
    {
        health = setHealth;
        damage = setDamage;
        speed = setSpeed;
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

    void Move()
    {
        transform.position = transform.position + direction * speed * Time.deltaTime;
        //rb.MovePosition(transform.position + direction * speed * Time.fixedDeltaTime);
    }
}
