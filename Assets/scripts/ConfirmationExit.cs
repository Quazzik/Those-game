using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class ConfirmationExit : MonoBehaviour
{
    public GameObject ESCMenuPrefab;
    public GameObject mainMenuPrefab;
    public bool levelExitMenu;

    private bool isMenuActive = false;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Destroy(gameObject.GetComponentInParent<Canvas>().gameObject);
        }
    }

    public void ToggleMenu()
    {
        isMenuActive = !isMenuActive;
        WantMore();
    }

    public void ReallyExit()
    {
        if (levelExitMenu)
        {
            SceneManager.LoadScene("MainMenu");
        }
        else
            Application.Quit();
    }

    public void WantMore()
    {
        if (levelExitMenu)
        {
            GameObject.FindWithTag("Player").GetComponent<Player>().ChangeMenu(true);
            Destroy(gameObject.GetComponentInParent<Canvas>().gameObject);
        }
        else
        {
            Instantiate(mainMenuPrefab);
            Destroy(gameObject.GetComponentInParent<Canvas>().gameObject);
        }
    }
}
