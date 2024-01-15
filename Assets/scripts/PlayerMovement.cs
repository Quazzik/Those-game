using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speedMultiplier = 5f; // Speed velocity
    public float jumpForce = 5f; // Jump force
    public Material material;
    public float rotationSpeed = 5f;

    void Update()
    {

        // Getting input from keyboard
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput);
        transform.Translate(moveDirection * Time.deltaTime * 5f);

        // Поворот куба с помощью мыши
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        transform.Rotate(Vector3.up, mouseX);

        // Calculating move direction
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * speedMultiplier * Time.deltaTime;

        // Moving object
        transform.Translate(movement);
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            Unstuck();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void Unstuck()
    {
        transform.rotation = Quaternion.identity;
        transform.position = new Vector3(0f,15f,0f);
    }

    void Jump()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, jumpForce, 0);
    }
    void OnCollisionEnter(Collision collision)
    {
        // Проверяем, столкнулся ли текущий объект с другим
        if (collision.gameObject.tag == "BulletTag")
        {
            Debug.Log("Столкновение с другим объектом");
            material.color = new Color(Random.value, Random.value, Random.value);
            var vector = gameObject.transform.localScale;

            //gameObject.transform.localScale = new Vector3(vector.x + 0.5f, vector.y + 0.5f, vector.z + 0.5f);
            // Дополнительная логика при столкновении
        }
    }

}
