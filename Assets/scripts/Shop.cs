using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public GameObject canvasPrefab;
    public GameObject contentContainer;
    public TextMeshProUGUI playerCoinsText;

    private int playerCoins;
    private float previousX = 20f;
    private void Start()
    {
        SpawnObjects();
        playerCoins = PlayerPrefs.GetInt("PlayerMoney", 0);
        playerCoinsText.text = $"Coins: {playerCoins}";
    }

    private void SpawnObjects()
    {
        int itemAmount = 15;
        SetContainerWidth(itemAmount);

        for (int i = 0; i < itemAmount; i++)
        {
            GameObject spawnedObject = Instantiate(canvasPrefab, contentContainer.transform);
            spawnedObject.transform.localScale = new Vector3 (2.66f, 2.66f, 2.66f);
            spawnedObject.transform.localPosition = new Vector3(previousX, -20, 0f);
            CreateNewX();

            TextMeshProUGUI textComponent = spawnedObject.GetComponentInChildren<TextMeshProUGUI>();
            if (textComponent != null)
            {
                textComponent.text = $"Object {i+1}";
            }
        }
    }

    private void CreateNewX()
    {
        previousX += 130 * 2.66f + 20;
    }

    private void SetContainerWidth(int itemAmoint)
    {
        var newSize = (float)(itemAmoint * 130 * 2.66) + (itemAmoint * 20);

        var size = contentContainer.GetComponent<RectTransform>().sizeDelta;
        size.x = newSize;
        contentContainer.GetComponent<RectTransform>().sizeDelta = size;
    }
}
