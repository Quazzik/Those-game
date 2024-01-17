using UnityEngine;

public class TurretHead : MonoBehaviour
{
    public Transform fireTarget; // Цель, на которую наводится голова
    public float rotationSpeed = 5f; // Скорость поворота башни

    void Update()
    {
        if (fireTarget != null)
        {
            // Получаем направление к игроку
            Vector3 directionToPlayer = fireTarget.position - transform.position;

            // Используем LookRotation для определения полного вращения в сторону игрока
            Quaternion rotation = Quaternion.LookRotation(directionToPlayer.normalized);

            // Получаем угол вращения по вертикали
            float verticalRotation = Mathf.Atan2(directionToPlayer.y, directionToPlayer.magnitude) * Mathf.Rad2Deg;

            // Используем Slerp для плавного поворота объекта в сторону игрока, сохраняя угол X
            Quaternion targetRotation = Quaternion.Euler(transform.eulerAngles.x, rotation.eulerAngles.y, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
