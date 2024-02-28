using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadMoneyText : MonoBehaviour
{
    private ControllerJSON json;
    public GameObject obj;
    /*private void Awake()
    {
        json = GetComponent<ControllerJSON>();
        json.LoadField();
    }*/
    private void Start()
    {
        json = obj.GetComponent<ControllerJSON>();
    }
    // Update is called once per frame
    void Update()
    {
        GetComponent<TMP_Text>().text = json.item.Money;
    }
}
