using UnityEngine;

public class Turret : MonoBehaviour
{
    private GameObject playerModel;
    [HideInInspector]
    public bool onGround = false;
    [HideInInspector]
    public float hp;
    private int otherHits = 0;
    private int playerHits = 0;
    [HideInInspector]
    public float xpBonus;

    private void Start()
    {
        playerModel = GameObject.FindWithTag("Player");
    }
    void Update()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
            if (playerHits > 0)
            {
                var xpForPlayer = playerHits / (playerHits + otherHits) * xpBonus;
                Player playerComponent = playerModel.GetComponent<Player>();
                playerComponent.playerXp += xpForPlayer*10;
            }
        }
        float distance = Vector3.Distance(transform.position, playerModel.transform.position);
        if (distance > 250)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Turret")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Ground")
        {
            onGround = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BulletTag")
        {
            ChangeAfterCollision(other);
            otherHits++;
        }
        else if (other.gameObject.tag == "reflectedBullet")
        {
            ChangeAfterCollision(other);
            playerHits++;
        }
    }
    private void ChangeAfterCollision(Collider other)
    {
        other.gameObject.GetComponent<Bullet>().toClear = true;
        hp--;
    }
}
