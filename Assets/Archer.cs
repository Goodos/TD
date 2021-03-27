using UnityEngine;

public class Archer : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    Collider target;
    bool CanShoot = false;
    float fireRate = 1;
    // Start is called before the first frame update
    void Start()
    {
        //Shoot();
    }

    // Update is called once per frame
    void Update()
    {
        if (fireRate <= 0)
        {
            target = CheckColls();
            if (target != null)
            {
                Shoot(target.transform.position);
            }
            fireRate = 1;
        }
        fireRate -= Time.deltaTime;
    }

    Collider CheckColls()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 40f);

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                if (rb.tag == "Enemy")
                    return hit;
            }
        }
        return null;
    }

    void Shoot(Vector3 target)
    {
        GameObject newProjectile = Instantiate(projectile);
        //newProjectile.GetComponent<Projectile>().damage = 5;
        newProjectile.transform.position = transform.position;
        newProjectile.GetComponent<Projectile>().SetParameters(target, 50f, 5);
        //newProjectile.transform.LookAt(target);
        //SimpleCannonBall(target, 100f, newProjectile);
        //CannonBall(target, 10f, newProjectile);
    }

    void SimpleCannonBall(Vector3 target, float speed, GameObject projectile)
    {
        Vector3 direction = (target - projectile.transform.position).normalized;
        projectile.transform.Translate(direction * speed * Time.deltaTime);// = projectile.transform.position + direction * speed * Time.deltaTime;
    }

    void CannonBall(Vector3 target, float initialAngle, GameObject projectile)
    {
        var rigid = projectile.GetComponent<Rigidbody>();

        Vector3 p = target;
        //Physics.gravity = new Vector3(0, -9.8f * jumpMulty, 0);
        float gravity = Physics.gravity.magnitude;
        //Debug.Log(Physics.gravity.magnitude);
        // Selected angle in radians
        float angle = initialAngle * Mathf.Deg2Rad;
        float initialVelocity;
        // Positions of this object and the target on the same plane

        Vector3 planarTarget = new Vector3(p.x, 0, p.z);
        Vector3 planarPostion = new Vector3(projectile.transform.position.x, 0, projectile.transform.position.z);

        // Planar distance between objects
        float distance = Vector3.Distance(planarTarget, planarPostion);
        // Distance along the y axis between objects
        float yOffset = projectile.transform.position.y - p.y;// - 2f);
        /*
        if (p.z - transform.position.z < 20f)
        {
            Physics.gravity *= 2f;
            initialVelocity = 1 / Mathf.Cos(70f) * Mathf.Sqrt((0.5f * gravity * 2f * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(70f) + yOffset));
            //Debug.Log("boom");
        }
        else*/
        initialVelocity = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));
        //Debug.Log(initialVelocity);
        Vector3 velocity = new Vector3(0, initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));

        // Rotate our velocity to match the direction between the two objects
        float angleBetweenObjects = Vector3.Angle(Vector3.forward, planarTarget - planarPostion) * (p.x > transform.position.x ? 1 : -1);
        Vector3 finalVelocity = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * velocity;

        // Fire!
        rigid.velocity = finalVelocity;


        // Alternative way:
        // rigid.AddForce(finalVelocity * rigid.mass, ForceMode.Impulse);
    }

}
