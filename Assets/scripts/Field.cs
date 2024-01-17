using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class Field : MonoBehaviour
{
    public float spawnSpeed = 10f;
    public GameObject turret1;
    public GameObject turret2;
    public GameObject turretBoss;
    public GameObject CubeSurvivor;




    private float timeDelta = 0f;

    void Update()
    {
        var newTime = Time.deltaTime;


        timeDelta += newTime;
        if (timeDelta >= spawnSpeed)
        {
            timeDelta = 0f;
            TryToSpawn();
        }


    }

    private void SpawnTurret(float cordX, float cordZ)
    {
        Vector3 position = new Vector3(cordX, 10, cordZ);
        var rotation = Quaternion.Euler(0, 1, 0);

        GameObject turret = Instantiate(turret1, position, rotation);

        var playerObject = GameObject.FindWithTag("Player");
        var playerTransform = playerObject.GetComponent<Transform>();

        TurretHead turretHD = turret.GetComponentInChildren<TurretHead>();
        turretHD.fireTarget = playerTransform;

        Bullet turretB = turret.GetComponentInChildren<Bullet>();
        turretB.deletedistance = 100;

        var turretTR = turret.GetComponent<Transform>();
        turretTR.Rotate(Vector3.up, GetRandomRotation());
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