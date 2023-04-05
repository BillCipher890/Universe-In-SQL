using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject buttonContinue;
    GameLoader gameLoader = new GameLoader();

    public void Start()
    {
        //���� ���� ���������� ���������� ������, �� ���������� ������ "����������"
        buttonContinue.SetActive(PlayerPrefs.HasKey("LastSceneIndex"));
    }

    public void clickButtonContinue()
    {
        gameLoader.load();
    }

    public void clickButtonNewGame()
    {
        PlayerPrefs.DeleteAll();
        gameLoader.loadLevel(1);
    }

    public void clickButtonExit()
    {
        Application.Quit();
    }
}
