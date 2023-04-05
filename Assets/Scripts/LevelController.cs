using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.SetInt("LastSceneIndex", SceneManager.GetActiveScene().buildIndex);
    }

    public void SaveCompletedLevel()
    {
        PlayerPrefs.SetInt("SaveCompletedLevelIndex", SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        SaveCompletedLevel();
        GameLoader gameLoader = new GameLoader();
        if (PlayerPrefs.HasKey("LastSceneIndex"))
        {
            gameLoader.loadLevel(PlayerPrefs.GetInt("LastSceneIndex") + 1);
        }
        else
        {
            Debug.LogError("PlayerPrefs has no key LastSceneIndex");
        }
    }
}
