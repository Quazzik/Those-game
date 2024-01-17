using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    public float speedMultiplier = 5f; // Speed velocity
    public float jumpForce = 5f; // Jump force
    public float rotationSpeed = 5f;
    public int hp = 2;
    public int defaultReflectionCooldown = 10;
    public int defaultReflectionActive = 2;
    public Material material;

    private bool godmode = false;
    private bool isGrounded = false;
    private float horizontalInput;
    private float verticalInput;
    private bool alive = true;

    private int hpSaver;
    private int playerLvl = 1;
    private float playerXp = 0;
    private float playerXpToNext = 120;
    private float summaryTime = 0f;

    private bool reflection = false;
    private float reflectionCountdownTimer = 0f;
    private float reflectionActiveTimer = 0f;

    public TextMeshProUGUI playerLvlText;
    public TextMeshProUGUI playerXpText;
    public TextMeshProUGUI totalTimeText;
    public TextMeshProUGUI ReflectionStatusText;
    public TextMeshProUGUI healthStatus;

    void Update()
    {
        if (godmode) { hp = 10000; alive = true; healthStatus.text = "Godmode"; healthStatus.color = Color.yellow; reflectionActiveTimer = 1000; reflectionCountdownTimer = 0; }
        if (alive)
        {
            #region other
            if (hp <= 0)
            {
                alive = false;
                healthStatus.color = Color.black;
            }
            else if (!godmode)
            {
                healthStatus.color = Color.white;
                healthStatus.text = "Health: " + hp.ToString();
            }
            var newTime = Time.deltaTime;
            reflectionCountdownTimer -= newTime;
            reflectionActiveTimer -= newTime;

            if (reflectionActiveTimer > 0)
            {
                reflection = true;
            }
            else
                reflection = false;
            #endregion

            #region UI
            if (reflection)
            {
                ReflectionStatusText.text = "Reflection active: " + reflectionActiveTimer.ToString("F1");
            }
            else
            {
                if (reflectionCountdownTimer > 0)
                {
                    ReflectionStatusText.text = "Reflection Cooldown: " + reflectionCountdownTimer.ToString("F1");
                }
                else
                    ReflectionStatusText.text = "Reflection Ready!";
            }

            summaryTime += newTime;
            TimeSpan timeSpan = TimeSpan.FromSeconds(summaryTime);
            totalTimeText.text = string.Format("Time alive:\n {0:D2}:{1:D2}", (int)timeSpan.TotalMinutes, timeSpan.Seconds);

            playerXp += newTime;
            playerXpText.text = "XP: " + playerXp.ToString("F0");
            if (playerXp >= playerXpToNext)
            {
                playerLvl++;
                playerXp -= playerXpToNext;
                playerXpToNext *= 2f;

                playerLvlText.text = "LVL: " + playerLvl.ToString("F0");
                playerXpText.text = "XP: " + playerXp.ToString("F0");
            }
            #endregion

            #region Keys
            if (Input.GetKeyDown(KeyCode.G))
            {
                if (!godmode)
                {
                    hpSaver = hp;
                    godmode = true;
                }
                else
                {
                    godmode = false;
                    hp = hpSaver;
                    reflectionActiveTimer = 2f;
                    reflectionCountdownTimer = 10f;
                }
            }

            if (reflectionCountdownTimer <= 0)
            {
                if (Input.GetKeyDown(KeyCode.LeftAlt))
                {
                    reflectionActiveTimer = defaultReflectionActive;
                    reflectionCountdownTimer = defaultReflectionCooldown;
                }
            }
            // Поворот куба с помощью мыши
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
            #endregion
        }
        else
        {
            healthStatus.text = "You dead";
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BulletTag"))
        {
            if (reflection)
            {
                var newVelocity = other.GetComponent<Rigidbody>().velocity * -1;
                other.GetComponent<Rigidbody>().velocity = newVelocity;

                other.GetComponent<Bullet>().toClear = false;
            }else
            {
                hp--;
                other.GetComponent<Bullet>().toClear = true;
            }
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
