using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class BoxDelete : MonoBehaviour
{
    public float fadeDuration = 1f;
    public GameObject wall1;
    public GameObject wall2;
    public GameObject wall3;
    public GameObject wall4;
    public GameObject wall5;
    public GameObject wall6;
    public Material wallMaterial;
    private Material newMaterial;
    private Color initialColor;
    private Color targetColor;
    void Start()
    {
        // копия мотериала
        newMaterial = new Material(wallMaterial);

        wall1.GetComponentInChildren<Renderer>().material = newMaterial;
        wall2.GetComponentInChildren<Renderer>().material = newMaterial;
        wall3.GetComponentInChildren<Renderer>().material = newMaterial;
        wall4.GetComponentInChildren<Renderer>().material = newMaterial;
        wall5.GetComponentInChildren<Renderer>().material = newMaterial;
        wall6.GetComponentInChildren<Renderer>().material = newMaterial;
        
        wallMaterial = newMaterial;

        initialColor = wallMaterial.color;

        targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0f);

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= 1)
        {
            StartCoroutine(FadeOut(newMaterial, wall1));
            StartCoroutine(FadeOut(newMaterial, wall2));
            StartCoroutine(FadeOut(newMaterial, wall3));
            StartCoroutine(FadeOut(newMaterial, wall4));
            StartCoroutine(FadeOut(newMaterial, wall5));
            StartCoroutine(FadeOut(newMaterial, wall6));
        }
    }
    private IEnumerator FadeOut(Material material, GameObject obj)
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(initialColor.a, targetColor.a, elapsedTime / fadeDuration);

            Color newColor = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);

            material.color = newColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Destroy(obj, 0.3f);
    }
}
