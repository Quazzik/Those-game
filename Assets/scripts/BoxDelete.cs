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
    public Material mat1;
    public Material mat2;
    public Material mat3;
    public Material mat4;
    public Material mat5;
    public Material mat6;
    public float step;
    Color color;
    private Color initialColor;
    private Color targetColor;
    private List<Renderer> walls;
    public List<Renderer> AddListRenderer(List<Renderer> first, List<Renderer> ex) 
    {
        foreach (var item in ex)
            first.Add(item);
        return first;
    }
    void Start()
    {
        /*wall1 = this.transform.GetChild(0).GetComponent<GameObject>();
        wall2 = this.transform.GetChild(1).GetComponent<GameObject>();
        wall3 = this.transform.GetChild(2).GetComponent<GameObject>();
        wall4 = this.transform.GetChild(3).GetComponent<GameObject>();
        wall5 = this.transform.GetChild(4).GetComponent<GameObject>();
        wall6 = this.transform.GetChild(5).GetComponent<GameObject>();
        color = wall1.GetComponent<Color>();*/
        walls = GetComponentsInChildren<Renderer>().ToList();

        List<Material> materials = new List<Material>();
        materials.Add(mat1);
        materials.Add(mat2);
        materials.Add(mat3);
        materials.Add(mat4);
        materials.Add(mat5);
        materials.Add(mat6);
        List<Renderer> renderers = new List<Renderer> ();
        foreach (var wall in walls)
        {
            renderers = AddListRenderer(renderers, wall.GetComponentsInChildren<Renderer>().ToList());
        }
        /*renderers = AddListRenderer(renderers, wall1.GetComponentsInChildren<Renderer>().ToList());
        renderers = AddListRenderer(renderers, wall2.GetComponentsInChildren<Renderer>().ToList());
        renderers = AddListRenderer(renderers, wall3.GetComponentsInChildren<Renderer>().ToList());
        renderers = AddListRenderer(renderers, wall4.GetComponentsInChildren<Renderer>().ToList());
        renderers = AddListRenderer(renderers, wall5.GetComponentsInChildren<Renderer>().ToList());
        renderers = AddListRenderer(renderers, wall6.GetComponentsInChildren<Renderer>().ToList());
*/
        foreach (Renderer renderer in renderers)
        {
            // Создаем копию оригинального материала
            Material newMaterial = new Material(mat1);

            // Назначаем новый материал для текущего объекта
            renderer.material = newMaterial;
            mat1 = renderer.material;
        }
        for (int i = 0; i < materials.Count; i++)
        {
            materials[i] = renderers[i].material;
        }
        mat1 = materials[0];
        mat2 = materials[1];
        mat3 = materials[2];
        mat4 = materials[3];
        mat5 = materials[4];
        mat6 = materials[5];

        initialColor = mat1.color;

        // ������������� ������� ���� � ��� �� �������������, �� � �����-������� ������ 0
        targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0f);

        // ��������� �������� ��� �������� ��������� �������
        /*StartCoroutine(FadeOut(mat1, wall1));
        StartCoroutine(FadeOut(mat2, wall2));
        StartCoroutine(FadeOut(mat3, wall3));
        StartCoroutine(FadeOut(mat4, wall4));
        StartCoroutine(FadeOut(mat5, wall5));
        StartCoroutine(FadeOut(mat6, wall6));*/
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y <= 1)
        {
                StartCoroutine(FadeOut(mat1, wall1));
                StartCoroutine(FadeOut(mat2, wall2));
                StartCoroutine(FadeOut(mat3, wall3));
                StartCoroutine(FadeOut(mat4, wall4));
                StartCoroutine(FadeOut(mat5, wall5));
                StartCoroutine(FadeOut(mat6, wall6));
        }
    }
    private IEnumerator FadeOut(Material material, GameObject obj)
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            // ������������� �������� �����-������ �� ���������� � ��������
            float alpha = Mathf.Lerp(initialColor.a, targetColor.a, elapsedTime / fadeDuration);

            // ������� ����� ���� � ���������� �����-�������
            Color newColor = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);

            // ������������� ����� ���� ���������
            material.color = newColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Destroy(obj);
    }
}
