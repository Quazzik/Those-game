using System.Collections.Generic;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class Field : MonoBehaviour
{
    public GameObject turret1;
    public GameObject turret2;
    public GameObject turret3;
    public GameObject CubeSurvivor;

    private GameObject playerObject;
    private Transform playerTransform;
    private int difficultyModifier;
    private float spawnTime = 1f;
    private float timeDelta = 50f;
    private int defaultMaxEnemiesCount = 5;

    private void Start()
    {
        playerObject = GameObject.FindWithTag("Player");
        playerTransform = playerObject.GetComponent<Transform>();
    }

    void Update()
    {
        var newTime = Time.deltaTime;

        difficultyModifier = playerObject.GetComponent<Player>().playerLvl;
        
        var enemiesCount = GameObject.FindGameObjectsWithTag("Turret").Length;

        timeDelta += newTime;
        if (timeDelta >= spawnTime)
        {
            if (enemiesCount < defaultMaxEnemiesCount + (difficultyModifier * 0.2f))
            {
                timeDelta = 0f;
                TryToSpawn();
                Debug.Log($"Max enemies: {defaultMaxEnemiesCount + (difficultyModifier * 0.2f)}");
            }
        }
    }

    private void SpawnTurret(float cordX, float cordZ)
    {
        Vector3 position = new Vector3(cordX, 10, cordZ);
        var rotation = Quaternion.Euler(0, 1, 0);

        var turretParameters = GetRandomTurret();
        var modifier = turretParameters.Item2;
        var turretType = turretParameters.Item1;
        GameObject turret = Instantiate(turretType, position, rotation);

        TurretHead turretHD = turret.GetComponentInChildren<TurretHead>();
        turretHD.fireTarget = playerTransform;
        
        var turretScript = turret.GetComponent<Turret>();
        if (turretType == turret1)
        {
            turretScript.hp = 1;
            turretScript.xpBonus = 10;
        }
        else if(turretType == turret2)
        {
            turretScript.hp = 2;
            turretScript.xpBonus = 35;
        }
        else if(turretType == turret3)
        {
            turretScript.hp = 10;
            turretScript.xpBonus = 100;
        }
        if (modifier > 1f)
        {
            turretScript.hp *= modifier;
            turretScript.xpBonus *= modifier;
        }

        Bullet turretB = turret.GetComponentInChildren<Bullet>();
        turretB.deletedistance = 100;

        var turretTR = turret.GetComponent<Transform>();
        turretTR.Rotate(Vector3.up, GetRandomRotation());
    }

    public (GameObject, float) GetRandomTurret()
    {
        int randomValue = Random.Range(1, 9);
        float modifiedValue = randomValue + difficultyModifier * 0.5f;

        if (modifiedValue <= 10)
        {
            return (turret1, 1f);
        }
        else if (modifiedValue <= 20)
        {
            return (turret2, 1f);
        }
        else if (modifiedValue <= 40)
        {
            return (turret3, 1f);
        }
        else
        {
            return (turret3, modifiedValue / 40f);
        }
    }

    private void TryToSpawn()
    {
        float randomAngle = Random.Range(0f, 2 * Mathf.PI);
        float radius = 30f;

        float randomX = radius * Mathf.Cos(randomAngle);
        float randomY = radius * Mathf.Sin(randomAngle);

        List <float> retcords = GetNotBlocked(randomX, randomY);
        SpawnTurret(retcords[0], retcords[1]);
    }

    private List<float> GetNotBlocked(float randomX, float randomY)
    {
        Collider[] colliders = Physics.OverlapSphere(new Vector3(randomX, 2, randomY), 3f);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Ground"))
            {
                continue;
            }

            float newX = randomX + Random.Range(-1f, 1f);
            float newY = randomY + Random.Range(-1f, 1f);

            return GetNotBlocked(newX, newY);
        }

        return new List<float> { randomX, randomY };
    }
    private float GetRandomRotation()
    {
        float rotation = Random.Range(0, 361);
        return rotation;
    }
}