using UnityEngine;

public class FirePoint : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    private float shootDelay;
    public float startShootDelay;
    public float standartShootDelay = 5.0f;

    void Start()
    {
        shootDelay = startShootDelay;
    }

    void Update()
    {
        if (gameObject.GetComponentInParent<Turret>().onGround)
        {
            shootDelay -= Time.deltaTime;
            if (gameObject.GetComponentInParent<TurretHead>().seePlayer)
            {
                if (shootDelay <= 0)
                {
                    shootDelay = standartShootDelay;
                    Shoot();
                }
            }
            else { shootDelay = startShootDelay; }
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.AddComponent<Rigidbody>();

        Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();
        bulletRB.useGravity = false;
        bulletRB.velocity = firePoint.forward * bulletSpeed;
    }
}
