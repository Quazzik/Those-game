using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class ClickButtonBuster : MonoBehaviour
{
    public List<Sprite> buttons;
    private int count;
    private int itemSprite;
    public GameObject obj;
    private ControllerJSON json;
    Dictionary<string, string> IsTag;
    private void Awake()
    {
        json = obj.GetComponent<ControllerJSON>();
        count = buttons.Count;
        //itemSprite = 0;
        IsTag = new Dictionary<string, string>()
        {
            {"ReflictionBuster", json.item.Reflection },
            {"ArmorBuster", json.item.Armor },
            {"HealsBuster", json.item.Heals },
            {"Bottle_blueBuster", json.item.Bottle_blue },
            {"Bottle_redBuster", json.item.Bottle_red },
            {"DamageBuster", json.item.Damage },
            {"Item_1Buster", json.item.Item_1 },
            {"Item_2Buster", json.item.Item_2 }
        };
        itemSprite = Convert.ToInt32(IsTag[this.tag]) * 2;
        ReadSprite();
    }

    private void OnMouseDown()
    {
        ReadSprite();
    }
    private void OnMouseUp()
    {
        if (Convert.ToUInt32(json.item.Money) >= itemSprite / 2 * 50)
        {
            SetFileTag();
            json.item.Money = (Convert.ToUInt32(json.item.Money) - itemSprite / 2 * 50).ToString();
        }
        else
            itemSprite -= 2;
        ReadSprite();
        json.SaveField();

    }
    private void ReadSprite()
    {
        if (itemSprite < count)
            GetComponent<SpriteRenderer>().sprite = buttons[itemSprite++];
    }
    private void SetFileTag()
    {
        if (gameObject.CompareTag("ReflictionBuster"))
            json.item.Reflection = (itemSprite / 2).ToString();
        if (gameObject.CompareTag("ArmorBuster"))
            json.item.Armor = (itemSprite / 2).ToString();
        if (gameObject.CompareTag("HealsBuster"))
            json.item.Heals = (itemSprite / 2).ToString();
        if (gameObject.CompareTag("Bottle_blueBuster"))
            json.item.Bottle_blue = (itemSprite / 2).ToString();
        if (gameObject.CompareTag("Bottle_redBuster"))
            json.item.Bottle_red = (itemSprite / 2).ToString();
        if (gameObject.CompareTag("DamageBuster"))
            json.item.Damage = (itemSprite / 2).ToString();
        if (gameObject.CompareTag("Item_1Buster"))
            json.item.Item_1 = (itemSprite / 2).ToString();
        if (gameObject.CompareTag("Item_2Buster"))
            json.item.Item_2 = (itemSprite / 2).ToString();
    }
}
