using UnityEngine;

public class TurretHead : MonoBehaviour
{
    public Transform fireTarget;
    public float rotationSpeed = 5f;
    public LayerMask obstacleMask;
    public bool seePlayer;

    void Update()
    {
        if (fireTarget != null)
        {
            Vector3 directionToPlayer = fireTarget.position - transform.position;

            Ray ray = new Ray(transform.position, directionToPlayer);

            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, 200, obstacleMask))
            {
                if (hitInfo.collider.tag != "Player")
                {
                    seePlayer = false;
                    return;
                }
                else
                {
                    seePlayer = true;
                }
            }

            Quaternion rotation = Quaternion.LookRotation(directionToPlayer.normalized);

            Quaternion targetRotation = Quaternion.Euler(transform.eulerAngles.x, rotation.eulerAngles.y, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
