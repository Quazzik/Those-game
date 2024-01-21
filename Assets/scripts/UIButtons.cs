using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour
{
    public GameObject confirmationMenuPrefab;

    public void SwitchScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ContinueGame()
    {
        var player = GameObject.FindWithTag("Player");
        player.GetComponent<Player>().ToggleMenu();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ExitGame()
    {
        Instantiate(confirmationMenuPrefab);
        Destroy(gameObject.GetComponentInParent<Canvas>().gameObject);
    }
}
