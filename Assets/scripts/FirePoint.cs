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

    public GameObject bulletPrefab; // ������ ���� (����)
    public Transform firePoint; // �����, ������ ����� ����������� ����
    public float bulletSpeed = 10f; // �������� �����
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
        // ������� ��������� ���� �� �������
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // �������� ��������� Rigidbody ����
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.AddComponent<Bullet>();

        // ��������� ���� � ���� ��� �������� ������ � ������������ ���������
        bulletRb.velocity = firePoint.forward * bulletSpeed;
    }
}
