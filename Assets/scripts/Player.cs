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
    private bool reverseDirection = false;

    void Update()
    {

        // ������� ���� � ������� ����
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        transform.Rotate(Vector3.up, mouseX);

        if (isGrounded)
        {
            Moving();
        }

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * speedMultiplier * Time.deltaTime;

        transform.Translate(movement);

        if (Input.GetKeyDown(KeyCode.R))
        {
            Unstuck();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            reverseDirection = !reverseDirection;
            Debug.Log($"Changed parameter to {reverseDirection}");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BulletTag"))
        {
            var newVelocity = other.GetComponent<Rigidbody>().velocity * -1;
            other.GetComponent<Rigidbody>().velocity = newVelocity;

            other.GetComponent<Bullet>().toClear = false;
            material.color = new Color(Random.value, Random.value, Random.value);
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
    }
}
