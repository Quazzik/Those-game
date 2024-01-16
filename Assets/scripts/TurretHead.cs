using Unity.VisualScripting;
using UnityEngine;

public class TurretHead : MonoBehaviour
{
    public Transform fireTarget; // Цель на которую наводится голова
    public float rotationSpeed = 5f; // Скорость поворота башни

    void Update()
    {
        // Проверяем нажатие клавиши для стрельбы (в данном случае, r)


        if (fireTarget != null)
        {
            // Получаем направление к игроку
            Vector3 directionToPlayer = fireTarget.position - transform.position;

            // Используем Slerp для плавного поворота объекта в сторону игрока
            Quaternion rotation = Quaternion.LookRotation(directionToPlayer.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
    }
}
