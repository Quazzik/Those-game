using Unity.VisualScripting;
using UnityEngine;

public class FirePoint : MonoBehaviour
{
    private bool pressed;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    private float shootDelay;
    public float startShootDelay;
    public float standartShootDelay = 5.0f;

    private void Start()
    {
        shootDelay = startShootDelay+standartShootDelay;
        startShootDelay = 0;
    }
    void Update()
    {
        shootDelay -= Time.deltaTime;
        if (shootDelay < 0)
        {
            shootDelay = standartShootDelay;
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.T)) { pressed = true; }
        if (Input.GetKeyUp(KeyCode.T)) { pressed = false; }

        if (pressed) Shoot();
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
