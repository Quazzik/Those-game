using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUpgrade : MonoBehaviour
{
    public string buster;
    private int itemUpgrade;
    private int price;
    public Sprite buttonClose;
    public Sprite buttonSelected;
    public TMP_Text textMeshPro;
    private SpriteState sprite;
    private void Start()
    {
        itemUpgrade = PlayerPrefs.GetInt(buster);
        price = (itemUpgrade + 1) * 50;
        textMeshPro.text = price.ToString();
        ButtonUpdate();
    }
    private void Update()
    {
        ButtonUpdate();
    }

    private void ButtonUpdate()
    {
        sprite = GetComponent<Button>().spriteState;
        if (PlayerPrefs.GetInt("money") >= price)
        {
            sprite.selectedSprite = buttonSelected;
            sprite.highlightedSprite = buttonSelected;
        }
        else
        {
            sprite.selectedSprite = buttonClose;
            sprite.highlightedSprite = buttonClose;
        }
        GetComponent<Button>().spriteState = sprite;
    }

    public void BuyItemPlayer() 
    {
        if (PlayerPrefs.GetInt("money") >= price)
        {
            MoneyAndItemUpdate();
            sprite.selectedSprite = buttonSelected;
            sprite.highlightedSprite = buttonSelected;
        }
        else
        {
            sprite.selectedSprite = buttonClose;
            sprite.highlightedSprite = buttonClose;
        }
        GetComponent<Button>().spriteState = sprite;
        textMeshPro.text = price.ToString();
        PlayerPrefs.SetInt(buster, itemUpgrade);
        Debug.Log(buster + " " + itemUpgrade / 2);
        Debug.Log(PlayerPrefs.GetInt("money"));
    }
    private void MoneyAndItemUpdate() 
    {
        PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money") - price);
        itemUpgrade++;
        price = (itemUpgrade + 1) * 50;
    }
}
