using UnityEngine;

public class TurretHead : MonoBehaviour
{
    public Transform fireTarget; 
    public float rotationSpeed = 5f;

    void Update()
    {
        if (fireTarget != null)
        {
            Vector3 directionToPlayer = fireTarget.position - transform.position;

            Quaternion rotation = Quaternion.LookRotation(directionToPlayer.normalized);

            Quaternion targetRotation = Quaternion.Euler(transform.eulerAngles.x, rotation.eulerAngles.y, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
