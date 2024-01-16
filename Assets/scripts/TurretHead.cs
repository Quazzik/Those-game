using Unity.VisualScripting;
using UnityEngine;

public class TurretHead : MonoBehaviour
{
    public Transform fireTarget; // ���� �� ������� ��������� ������
    public float rotationSpeed = 5f; // �������� �������� �����

    void Update()
    {
        // ��������� ������� ������� ��� �������� (� ������ ������, r)


        if (fireTarget != null)
        {
            // �������� ����������� � ������
            Vector3 directionToPlayer = fireTarget.position - transform.position;

            // ���������� Slerp ��� �������� �������� ������� � ������� ������
            Quaternion rotation = Quaternion.LookRotation(directionToPlayer.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
    }
}
