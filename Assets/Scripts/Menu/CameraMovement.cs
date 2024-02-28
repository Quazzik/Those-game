using UnityEngine;

public class AutoCameraRotation : MonoBehaviour
{
    public Transform target;
    public float rotationSpeed = 5f;

    void Update()
    {
        RotateAroundTarget();
    }

    void RotateAroundTarget()
    {
        float angle = rotationSpeed * Time.deltaTime;

        transform.RotateAround(target.position, Vector3.up, angle);
    }
}
