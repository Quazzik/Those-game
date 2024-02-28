using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfirmationExit : MonoBehaviour
{
    public GameObject ESCMenuPrefab;
    public GameObject mainMenuPrefab;
    public bool levelExitMenu;
    public GameObject player;
    [HideInInspector]
    public bool onMainMeu = false;
    private bool isPause = false;
    private GameObject viewPrefab;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause) 
            {
                ClearPrefab();
                DownPause();
            }
            else 
            {
                SelectPrefab();
                ActivatedPrefab();
                OnPause();
            }
        }
    }
    public void ButtonYesReadyExit() 
    {
        ClearPrefab();
        SelectPrefab();
        ActivatedPrefab();
    }
    public void ButtonNoReadyExit() 
    {
        ClearPrefab();
        DownPause();
    }
    public void ButtonYesAlreadyExit() 
    {
        LoadMainMenu();
        OnPause();
    }
    public void ButtonNoAlreadyExit() 
    {
        ClearPrefab();
        DownPause();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void ClearPrefab() 
    { 
        viewPrefab.SetActive(false);
    }
    public void ActivatedPrefab()
    {
        viewPrefab.SetActive(true);
    }
    public void OnPause() 
    {
        player.GetComponent<Player>().ChangeMenu(true);
        isPause = true;
        Time.timeScale = 0f;
        Cursor.visible = true;
    }
    public void DownPause() 
    {
        player.GetComponent<Player>().ChangeMenu(false);
        isPause = false;
        Time.timeScale = 1f;
        Cursor.visible = false;
    }
    public void PopViewPrefab(GameObject menuPrefab) 
    {
        viewPrefab = menuPrefab;
    }
    public void SelectPrefab() 
    {
        if (isPause) 
        {
            viewPrefab = mainMenuPrefab;
        }
        else
        {
            viewPrefab = ESCMenuPrefab;
        }
    }
}
