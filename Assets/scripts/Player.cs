using UnityEngine;

public class Player : MonoBehaviour
{
    public float speedMultiplier = 5f; // Speed velocity
    public float jumpForce = 5f; // Jump force
    public Material material;
    public float rotationSpeed = 5f;
    private bool isGrounded = false;
    private float horizontalInput;
    private float verticalInput;

    void Update()
    {

        // Поворот куба с помощью мыши
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        transform.Rotate(Vector3.up, mouseX);

        if (isGrounded)
        {
            Moving();
        }

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * speedMultiplier * Time.deltaTime;

        // Moving object
        transform.Translate(movement);

        if (Input.GetKeyDown(KeyCode.R))
        {
            Unstuck();
        }
    }

    private void Moving()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput);
        transform.Translate(moveDirection * Time.deltaTime * 5f);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
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
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }

        // Проверяем, столкнулся ли текущий объект с другим
        if (collision.gameObject.tag == "BulletTag")
        {
            material.color = new Color(Random.value, Random.value, Random.value);
            var vector = gameObject.transform.localScale;

            //gameObject.transform.localScale = new Vector3(vector.x + 0.5f, vector.y + 0.5f, vector.z + 0.5f);
            // Дополнительная логика при столкновении
        }
    }
}
