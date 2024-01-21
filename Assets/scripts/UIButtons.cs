using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour
{
    //public bool levelExitMenu;
    public GameObject confirmationMenuPrefab;

    public void SwitchScene()
    {
        var confirmationMenu = Instantiate(confirmationMenuPrefab);
        Destroy(gameObject.GetComponentInParent<Canvas>().gameObject);
        ConfirmationExit[] scripts = confirmationMenu.GetComponentsInChildren<ConfirmationExit>();
        foreach(var script in scripts)
        {
            script.levelExitMenu = true;
        }
    }

    public void ContinueGame()
    {
        var player = GameObject.FindWithTag("Player");
        player.GetComponent<Player>().ChangeMenu(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ExitGame()
    {
        var confirmationMenu = Instantiate(confirmationMenuPrefab);
        ConfirmationExit[] scripts = confirmationMenu.GetComponentsInChildren<ConfirmationExit>();
        foreach (var script in scripts)
        {
            script.levelExitMenu = false;
        }
        Destroy(gameObject.GetComponentInParent<Canvas>().gameObject);
    }
}
