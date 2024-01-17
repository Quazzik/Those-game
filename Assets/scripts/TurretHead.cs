using UnityEngine;

public class TurretHead : MonoBehaviour
{
    public Transform fireTarget; // ����, �� ������� ��������� ������
    public float rotationSpeed = 5f; // �������� �������� �����

    void Update()
    {
        if (fireTarget != null)
        {
            // �������� ����������� � ������
            Vector3 directionToPlayer = fireTarget.position - transform.position;

            // ���������� LookRotation ��� ����������� ������� �������� � ������� ������
            Quaternion rotation = Quaternion.LookRotation(directionToPlayer.normalized);

            // �������� ���� �������� �� ���������
            float verticalRotation = Mathf.Atan2(directionToPlayer.y, directionToPlayer.magnitude) * Mathf.Rad2Deg;

            // ���������� Slerp ��� �������� �������� ������� � ������� ������, �������� ���� X
            Quaternion targetRotation = Quaternion.Euler(transform.eulerAngles.x, rotation.eulerAngles.y, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
