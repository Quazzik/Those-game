using Unity.VisualScripting;
using UnityEngine;

public class CubeShooter : MonoBehaviour
{
    public GameObject bulletPrefab; // Префаб шара (пули)
    public Transform firePoint; // Точка, откуда будут выпускаться шары
    public float bulletSpeed = 10f; // Скорость шаров
    private bool pressed = false;

    void Update()
    {
        // Проверяем нажатие клавиши для стрельбы (в данном случае, r)
        if (Input.GetKeyDown(KeyCode.R))
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.T)) { pressed = true; }
        if (Input.GetKeyUp(KeyCode.T)) { pressed = false; }
        
        if (pressed) Shoot();
    }

    void Shoot()
    {
        // Создаем экземпляр шара из префаба
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Получаем компонент Rigidbody шара
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.AddComponent<BulletDelete>();

        // Применяем силу к шару для движения вперед с определенной скоростью
        bulletRb.velocity = firePoint.forward * bulletSpeed;
    }
}
