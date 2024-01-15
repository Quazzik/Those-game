using System;
using UnityEngine;

public class BulletDelete : MonoBehaviour
{
    void Update()
    {
        var position = transform.position;
        if (Math.Abs(position.y) > 13 || Math.Abs(position.x) > 13 || Math.Abs(position.z) > 13)
        {
            Destroy(gameObject);
        }
    }
}