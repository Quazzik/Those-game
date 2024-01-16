using System;
using UnityEngine;
public class Bullet : MonoBehaviour
{
    private float lifetime = 5f;
    private bool toClear = false;
    private float startX;
    private float startY;
    private float startZ;

    public float deletedistance = 100f;

    private void Start()
    {
        startX = transform.position.x;
        startY = transform.position.y;
        startZ = transform.position.z;
    }
    void Update()
    {
        var position = transform.position;
        if (ABSDiff(gameObject.transform.position.x,startX) ||
            ABSDiff(gameObject.transform.position.y, startY) ||
            ABSDiff(gameObject.transform.position.z, startZ))
        {
            Destroy(gameObject);
        }

        lifetime -= Time.deltaTime;

        if (toClear)
        {
            lifetime -= Time.deltaTime;
            if (lifetime < 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Turret" && collision.gameObject.tag != "BulletTag")
        {
            toClear = true;
        }
    }

    private bool ABSDiff(float a, float b)
    {
        var absa = Mathf.Abs(a);
        var absb = Mathf.Abs(b);
        if (Mathf.Abs(absa-absb) > deletedistance) 
            return true;
        return false;
    }
}