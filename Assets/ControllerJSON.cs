using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ControllerJSON : MonoBehaviour
{
    public Item item;
    private void Start()
    {
        this.LoadField();
    }
    [ContextMenu("Load")]
    public void LoadField()
    {
        item = JsonUtility.FromJson<Item>(File.ReadAllText(Application.streamingAssetsPath + "/MoneyAndBust.json"));
    }
    [ContextMenu("Save")]
    public void SaveField()
    {
        File.WriteAllText(Application.streamingAssetsPath + "/MoneyAndBust.json", JsonUtility.ToJson(item));
    }
    [System.Serializable]
    public class Item
    {
        public string Money;
        public string Reflection;
        public string Armor;
        public string Heals;
        public string Bottle_blue;
        public string Bottle_red;
        public string Damage;
        public string Item_1;
        public string Item_2;
    }
}
