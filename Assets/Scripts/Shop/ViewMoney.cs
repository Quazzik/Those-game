using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ViewMoney : MonoBehaviour
{
    private void Start()
    {
        MoneyUpdate();
    }
    public void MoneyUpdate() 
    {
        GetComponent<TMP_Text>().text = PlayerPrefs.GetInt("money").ToString();
    }
}
