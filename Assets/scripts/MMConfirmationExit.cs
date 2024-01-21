using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMConfirmationExit : MonoBehaviour
{
    public GameObject mainMenuPrefab;

    public void ReallyExit()
    {
        Application.Quit();
    }

    public void WantMore()
    {
        Instantiate(mainMenuPrefab);
        Destroy(gameObject.GetComponentInParent<Canvas>().gameObject);
    }
}
