using System;
using TMPro;
using UnityEngine;

public class GetDataInUI : MonoBehaviour
{
    public TextMeshProUGUI playerRecordText;
    public TextMeshProUGUI playerMoneyText;

    void Start()
    {
        TimeSpan playerRecod = TimeSpan.FromSeconds(PlayerPrefs.GetFloat("SurvivalTime"));

        playerRecordText.text = string.Format("Record: {0:D2}:{1:D2}", (int)playerRecod.TotalMinutes, playerRecod.Seconds);
        playerMoneyText.text = ($"Money: {PlayerPrefs.GetInt("money")}");
    }
}
