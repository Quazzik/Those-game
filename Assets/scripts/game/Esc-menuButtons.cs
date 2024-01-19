using UnityEngine;
using UnityEngine.SceneManagement;

public class IngameUIButtons : MonoBehaviour
{
    public void SwitchScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ContinueGame()
    {
        var player = GameObject.FindWithTag("Player");
        player.GetComponent<Player>().ToggleMenu();
    }
}
