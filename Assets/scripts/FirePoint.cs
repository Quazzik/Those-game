using Unity.VisualScripting;
using UnityEngine;

public class FirePoint : MonoBehaviour
{
    private bool pressed;

    public GameObject bulletPrefab; // Префаб шара (пули)
    public Transform firePoint; // Точка, откуда будут выпускаться шары
    public float bulletSpeed = 10f; // Скорость шаров
    private float shootDelay; // Текущая задержка между выстрелами
    public float startShootDelay; // Начальная задержка между выстрелами
    public float standartShootDelay = 5.0f; // Стандартная задержка между выстрелами

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
