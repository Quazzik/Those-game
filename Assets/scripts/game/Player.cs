using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    [HideInInspector]
    public float speedMultiplier = 5f;
    [HideInInspector]
    public float jumpForce = 5f;
    [HideInInspector]
    public float rotationSpeed = 5f;
    [HideInInspector]
    public int hp;
    public int defaultMaxHP;
    public float defaultRegenTime;
    public float defaultReflectionCooldown;
    public float defaultReflectionActive;
    public Material material;

    private bool isMenuActive = false;
    private bool godmode = false;
    private bool isGrounded = false;
    private float horizontalInput;
    private float verticalInput;
    [HideInInspector]
    public bool alive = true;

    private int totalMinutes = 1;
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
    public TextMeshProUGUI healthStatusText;
    public TextMeshProUGUI lvlUpText;
    public TextMeshProUGUI SurvivalTimeBonusText;

    public Animator animator;

    private ControllerJSON json;
    public GameObject obj;

    private void Start()
    {
        animator = GetComponent<Animator>();
        json = obj.GetComponent<ControllerJSON>();
        defaultMaxHP = PlayerPrefs.GetInt("defaultMaxHP", 2 * Convert.ToInt32(json.item.Heals));
        defaultRegenTime = PlayerPrefs.GetFloat("defaultRegenTime", 150f);
        defaultReflectionCooldown = PlayerPrefs.GetFloat("defaultReflectionCooldown", 15f);
        defaultReflectionActive = PlayerPrefs.GetFloat("defaultReflectionActive", 1.5f);

        hp = defaultMaxHP;
        untilRegenTime = defaultRegenTime;
        lvlUpText.color = new Color(255, 255, 255, 0);
        SurvivalTimeBonusText.color = new Color(255, 255, 255, 0);
        Time.timeScale = 1f;
        Cursor.visible = false;
    }
    void Update()
    {

        if (godmode) { hp = 10000; alive = true;
            healthStatusText.text = "Godmode";
            healthStatusText.color = Color.yellow;
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

            if (summaryTime / 60 > totalMinutes)
            {
                int money = PlayerPrefs.GetInt("PlayerMoney");
                PlayerPrefs.SetInt("PlayerMoney", money + 25);

                totalMinutes++;
                StartCoroutine(FadeText(SurvivalTimeBonusText,5));
            }

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
                healthStatusText.color = Color.black;
                EndGame();
            }
            else if (!godmode)
            {
                healthStatusText.color = Color.white;
                healthStatusText.text = "Health: " + hp.ToString() + "/" + defaultMaxHP.ToString();
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
            healthStatusText.text = "You dead";
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
        if (hp < defaultMaxHP)
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
            lvlUpText.text = string.Format("New record! \n Was {0:D2}:{1:D2},",
                (int)toptimeMins.TotalMinutes, toptimeMins.Seconds) +
                 string.Format("and now {0:D2}:{1:D2}",
                 (int)newTimeMins.TotalMinutes, newTimeMins.Seconds);

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
                defaultMaxHP++;
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
        DisplayLevelUpText(levelBonus);
    }

    public void DisplayLevelUpText(string levelBonus)
    {
        lvlUpText.text = "Level up: " + playerLvl +
            "\n" + levelBonus;

        StartCoroutine(FadeText(lvlUpText, 3f));
    }

    IEnumerator FadeText(TextMeshProUGUI textToFade, float duration)
    {
        textToFade.color = new Color(255, 255, 255, 255);
        float elapsedTime = 0f;
        Color startColor = textToFade.color;

        while (elapsedTime < duration)
        {
            float alpha = 1f - (elapsedTime / duration);
            textToFade.color = new Color(startColor.r, startColor.g, startColor.b, alpha);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        textToFade.color = new Color(startColor.r, startColor.g, startColor.b, 0f);
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
            animator.SetBool("IsMove", true);
        }
        if (verticalInput == 0)
        {
            animator.SetBool("IsMove", false);
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