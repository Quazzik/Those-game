using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopButtons : MonoBehaviour
{
    public void GoBack()
    {
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Jopa");
    }
}
