using UnityEngine;
using UnityEngine.UIElements;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float distance = 5f;
    public float baseRotationSpeed = 10f;
    public float zoomSpeed = 2f;

    private float rotationSpeed;
    private float verticalAngle = 30f;

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distance -= scroll * zoomSpeed * 1250 * Time.deltaTime;
        distance = Mathf.Clamp(distance, 2f, 10f);

        float targetRotationAngle = target.eulerAngles.y;

        float currentRotationAngle = transform.eulerAngles.y;
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, targetRotationAngle, rotationSpeed * Time.deltaTime);

        Quaternion rotation = Quaternion.Euler(verticalAngle, currentRotationAngle, 0f);

        Vector3 cameraPosition = target.position - rotation * Vector3.forward * distance;

        transform.position = cameraPosition;




        //verticalAngle += mouseX * Time.deltaTime * 200;

        // Отражаем движение мыши по оси Y на вертикальном угле поворота камеры
        float mouseY = Input.GetAxis("Mouse Y");
        verticalAngle -= mouseY * rotationSpeed * 100 * Time.deltaTime;
        verticalAngle = Mathf.Clamp(verticalAngle, -0f, 60f);

        transform.LookAt(target.position);
    }

    void LateUpdate()
    {
        rotationSpeed = baseRotationSpeed * PlayerPrefs.GetFloat("CameraRotationSpeed", 1f);
        rotationSpeed = Mathf.Clamp(rotationSpeed, 0.1f, 10f);
    }
}