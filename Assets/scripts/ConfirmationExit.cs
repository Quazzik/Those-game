using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfirmationExit : MonoBehaviour
{
    public GameObject ESCMenuPrefab;
    public GameObject mainMenuPrefab;
    public bool levelExitMenu;
    [HideInInspector]
    public bool onMainMeu = false;
    private bool isMenuActive = false;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            WantMore();
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
