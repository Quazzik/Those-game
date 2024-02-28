using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickBack : MonoBehaviour
{
    public void LoadMainScene()
    {
        SceneManager.LoadScene("MainMenu");
        Application.Quit();
    }
}
