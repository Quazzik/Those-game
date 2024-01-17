using UnityEngine;

public class Turret : MonoBehaviour
{
    private GameObject CubeSurvivor;
    [HideInInspector]
    public float hp;
    private int otherHits = 0;
    private int playerHits = 0;
    [HideInInspector]
    public float xpBonus;

    private void Start()
    {
        CubeSurvivor = GameObject.FindWithTag("Player");
    }
    void Update()
    {
        float currentAngleZ = transform.rotation.eulerAngles.z;
        if (currentAngleZ > 10)
        {
            Destroy(gameObject);
        }

        if (hp <= 0)
        {
            Destroy(gameObject);
            Debug.Log($"Player hits: {playerHits}, other hits: {otherHits}");
            if (playerHits > 0)
            {
                var xpForPlayer = playerHits / (playerHits + otherHits) * xpBonus;
                Player playerComponent = CubeSurvivor.GetComponent<Player>();
                playerComponent.playerXp += xpForPlayer;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BulletTag")
        {
            ChangeAfterCollision(other);
            otherHits++;
            Debug.Log("Hit from other");
        }
        else if (other.gameObject.tag == "reflectedBullet")
        {
            ChangeAfterCollision(other);
            playerHits++;
            Debug.Log("Hit from player");
        }
    }
    private void ChangeAfterCollision(Collider other)
    {
        other.gameObject.GetComponent<Bullet>().toClear = true;
        hp--;
    }
}
