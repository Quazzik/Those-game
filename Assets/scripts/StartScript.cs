using UnityEngine;

public class StartScript : MonoBehaviour
{
    public GameObject mainMenuPrefab;
    [HideInInspector]
    public GameObject mainMenu;

    void Start()
    {
        mainMenu = Instantiate(mainMenuPrefab);
    }
}