using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Transform sun;
    public float rotationSpeed;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        RotateLight(mouseX);
    }

    void RotateLight(float mouseX)
    {
        float rotation = mouseX * rotationSpeed;

        float currentRotationX = sun.rotation.eulerAngles.x;
        float currentRotationZ = sun.rotation.eulerAngles.z;

        Quaternion newRotation = Quaternion.Euler(currentRotationX, sun.rotation.eulerAngles.y + rotation, currentRotationZ);
        sun.rotation = newRotation;
    }
}
