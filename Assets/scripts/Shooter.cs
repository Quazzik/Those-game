using Unity.VisualScripting;
using UnityEngine;

public class CubeShooter : MonoBehaviour
{
    public GameObject bulletPrefab; // ������ ���� (����)
    public Transform firePoint; // �����, ������ ����� ����������� ����
    public float bulletSpeed = 10f; // �������� �����
    private bool pressed = false;

    void Update()
    {
        // ��������� ������� ������� ��� �������� (� ������ ������, r)
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
        // ������� ��������� ���� �� �������
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // �������� ��������� Rigidbody ����
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.AddComponent<BulletDelete>();

        // ��������� ���� � ���� ��� �������� ������ � ������������ ���������
        bulletRb.velocity = firePoint.forward * bulletSpeed;
    }
}
