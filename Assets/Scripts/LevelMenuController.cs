using UnityEngine;

public class LevelMenuController : MonoBehaviour
{
    [SerializeField] private GameObject buttonMenu;
    [SerializeField] private GameObject menuPanel;

    void Start()
    {
        buttonMenu.SetActive(true);
        menuPanel.SetActive(false);
    }

    public void clickButtonMenu()
    {
        buttonMenu.SetActive(false);
        menuPanel.SetActive(true);
    }

    public void clickButtonBack()
    {
        buttonMenu.SetActive(true);
        menuPanel.SetActive(false);
    }

    public void clickButtonMainMenu()
    {
        GameLoader gameLoader = new GameLoader();
        gameLoader.loadMenu();
    }

    public void clickButtonExit()
    {
        Application.Quit();
    }
}