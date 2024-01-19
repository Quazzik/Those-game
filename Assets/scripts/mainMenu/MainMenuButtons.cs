using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIButtons : MonoBehaviour
{
    public void SwitchScene()
    {
        SceneManager.LoadScene("Game");
    }
    public void ExitGame()
    {

    }
}
