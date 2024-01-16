using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEditor.Presets;
using UnityEngine;

public class FirePoint : MonoBehaviour
{
    private bool pressed;
    private float nowTime = 0;

    public GameObject bulletPrefab; // Префаб шара (пули)
    public Transform firePoint; // Точка, откуда будут выпускаться шары
    public float bulletSpeed = 10f; // Скорость шаров
    public float shootDelay = 5f;

    void Update()
    {
        nowTime += Time.deltaTime;
        if (nowTime >= shootDelay)
        {
            nowTime = 0;
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
        bulletRb.AddComponent<Bullet>();

        // Применяем силу к шару для движения вперед с определенной скоростью
        bulletRb.velocity = firePoint.forward * bulletSpeed;
    }
}
