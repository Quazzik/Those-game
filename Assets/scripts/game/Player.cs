using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    public float speedMultiplier = 5f;
    public float jumpForce = 5f;
    public float rotationSpeed = 5f;
    public int hp;
    public int maxHP = 2;
    private float defaultRegenTime = 120f;
    public int defaultReflectionCooldown = 10;
    public int defaultReflectionActive = 2;
    public Material material;

    private bool isMenuActive = false;
    private bool godmode = false;
    private bool isGrounded = false;
    private float horizontalInput;
    private float verticalInput;
    [HideInInspector]
    public bool alive = true;

    private float untilRegenTime;
    private int hpSaver;
    [HideInInspector]
    public int playerLvl = 1;
    [HideInInspector]
    public float playerXp = 0f;
    private float playerXpToNext = 100f;
    private float summaryTime = 0f;

    private bool reflection = false;
    private float reflectionCountdownTimer = 0f;
    private float reflectionActiveTimer = 0f;
    private GameObject ingameMenuPanel;

    public GameObject menuPanel;
    public TextMeshProUGUI playerLvlText;
    public TextMeshProUGUI playerXpText;
    public TextMeshProUGUI totalTimeText;
    public TextMeshProUGUI ReflectionStatusText;
    public TextMeshProUGUI healthStatus;
    public TextMeshProUGUI lvlUpText;

    // Add animation
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        hp = maxHP;
        untilRegenTime = defaultRegenTime;
        lvlUpText.gameObject.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
    }
    void Update()
    {

        if (godmode) { hp = 10000; alive = true;
            healthStatus.text = "Godmode";
            healthStatus.color = Color.yellow;
            reflectionActiveTimer = 1000;
            reflectionCountdownTimer = 0; }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (ingameMenuPanel != null)
            {
                ChangeMenu(false);
            }
            else
                ChangeMenu(true);
        }

        if (alive)
        {
            #region other

            var newTime = Time.deltaTime;
            untilRegenTime -= newTime;
            reflectionCountdownTimer -= newTime;
            reflectionActiveTimer -= newTime;

            if (!isMenuActive)
            {
                float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
                transform.Rotate(Vector3.up, mouseX);
            }

            if (isGrounded)
            {
                Moving();
            }

            if (untilRegenTime < 0)
            {
                Heal(1);
                untilRegenTime = defaultRegenTime;
            }

            if (reflectionActiveTimer > 0)
            {
                reflection = true;
            }
            else
                reflection = false;
            #endregion

            #region UI

            if (hp <= 0)
            {
                alive = false;
                healthStatus.color = Color.black;
                EndGame();
            }
            else if (!godmode)
            {
                healthStatus.color = Color.white;
                healthStatus.text = "Health: " + hp.ToString() + "/" + maxHP.ToString();
            }

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
            playerXpText.text = "XP: " + playerXp.ToString("F0")+ "/" + playerXpToNext.ToString("F0");
            if (playerXp >= playerXpToNext)
            {
                playerLvl++;
                GetLvlUpBonus();
                playerXp -= playerXpToNext;
                playerXpToNext *= 1.1f;

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
                    reflectionCountdownTimer = defaultReflectionCooldown + defaultReflectionActive;
                }
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
            Cursor.visible = true;
        }
    }

    public void ChangeMenu(bool whatto)
    {

        if (whatto)
        {
            isMenuActive = true;
            Time.timeScale = 0f;
            Cursor.visible = true;
            ingameMenuPanel = Instantiate(menuPanel);
        }
        else
        {
            isMenuActive = false;
            Time.timeScale = 1f;
            Cursor.visible = false;
            Destroy(ingameMenuPanel);
        }
    }

    private void Heal(int healCount)
    {
        if (hp < maxHP)
        {
            hp += healCount;
        }
    }

    private void EndGame()
    {
        var toptimeSeconds = PlayerPrefs.GetFloat("SurvivalTime");

        if (toptimeSeconds < summaryTime)
        {
            PlayerPrefs.SetFloat("SurvivalTime", summaryTime);
            PlayerPrefs.Save();

            var toptimeMins = TimeSpan.FromSeconds(toptimeSeconds);
            var newTimeMins = TimeSpan.FromSeconds(summaryTime);
            lvlUpText.text = string.Format("New record! \n Was {0:D2}:{1:D2}, and now it's ", (int)toptimeMins.TotalMinutes, toptimeMins.Seconds) +
                 string.Format("and now {0:D2}:{1:D2}", (int)newTimeMins.TotalMinutes, newTimeMins.Seconds);

            lvlUpText.rectTransform.sizeDelta = new Vector2(900, lvlUpText.rectTransform.sizeDelta.y);
            lvlUpText.gameObject.SetActive(true);
            lvlUpText.fontSize = 40;
            lvlUpText.color = new Color(1f, 1f, 1f, 1f);

        }
    }

    private void GetLvlUpBonus()
    {
        var bonusNum = Random.Range(0, 6);
        switch (bonusNum)
        {
            case 0:
                maxHP++;
                ShowLevelUpText("Max hp increased");
                break;
            case 1:
                if (playerLvl >= 15)
                {
                    playerLvl -= 2;
                    playerXpToNext /= 1.1f;
                    ShowLevelUpText("Player lvl reduced");
                }
                else
                {
                    int money = PlayerPrefs.GetInt("PlayerMoney");
                    PlayerPrefs.SetInt("PlayerMoney", money + 50);
                    PlayerPrefs.Save();
                    GiveCompensation("Player level is too low.");
                }
                break;
            case 2:
                speedMultiplier++;
                ShowLevelUpText("Player speed increased");
                break;
            case 3:
                if (defaultReflectionCooldown >= 5)
                {
                    defaultReflectionCooldown--;
                    ShowLevelUpText("Reflection cooldown reduced");
                }
                else
                {
                    int money = PlayerPrefs.GetInt("PlayerMoney");
                    PlayerPrefs.SetInt("PlayerMoney", money + 50);
                    PlayerPrefs.Save();
                    GiveCompensation("Reflection cooldown minimized.");
                }
                break;
            case 4:
                if (defaultReflectionActive <= 5)
                {
                    defaultReflectionActive++;
                    ShowLevelUpText("Reflection time increased");
                }
                else
                {
                    int money = PlayerPrefs.GetInt("PlayerMoney");
                    PlayerPrefs.SetInt("PlayerMoney", money + 50);
                    PlayerPrefs.Save();
                    GiveCompensation("Reflection time maximized.");
                }
                break;
            case 5:
                defaultRegenTime/=1.2f;
                ShowLevelUpText("Regeneration increased");
                break;
        }
    }

    public void GiveCompensation(string text)
    {
        ShowLevelUpText(text + "  You have received 50 coins of compensation");
    }

    public void ShowLevelUpText(string levelBonus)
    {
        StartCoroutine(DisplayLevelUpText(levelBonus));
    }

    IEnumerator DisplayLevelUpText(string levelBonus)
    {
        lvlUpText.text = "Level up: " + playerLvl +
            "\n" + levelBonus;
        lvlUpText.gameObject.SetActive(true);


        yield return new WaitForSeconds(3);
        lvlUpText.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BulletTag"))
        {
            if (reflection)
            {
                var newVelocity = other.GetComponent<Rigidbody>().velocity * -1;
                other.GetComponent<Rigidbody>().velocity = newVelocity;
                other.tag = "reflectedBullet";

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

        if (Math.Abs(verticalInput) > 0) 
        {
            animator.SetBool("IsMoving", true);
        }
        if (verticalInput == 0)
        {
            animator.SetBool("IsMoving", false);
        }

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
