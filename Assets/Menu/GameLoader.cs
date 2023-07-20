using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader
{
    public void load()
    {
        int sceneIndex = 1;
        if (PlayerPrefs.HasKey("LastSceneIndex"))
        {
            sceneIndex = PlayerPrefs.GetInt("LastSceneIndex");
        }

        SceneManager.LoadScene(sceneIndex);
    }

    public void loadLevel(int lvl)
    {
        SceneManager.LoadScene(lvl);
    }

    public void loadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public override string ToString()
    {
        return "gameLoader";
    }
}
