using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoaderLearn : MonoBehaviour
{
    private int sceneIndex = 1;

    public void onLoadClick()
    {
        if (PlayerPrefs.HasKey("SceneIndex"))
        {
            sceneIndex = PlayerPrefs.GetInt("SceneIndex");
        }

        SceneManager.LoadScene(sceneIndex);
    }

    public void Exit() 
    {
        Application.Quit(); 
    }

    public void SaveGame()
    {
        PlayerPrefs.SetInt("SceneIndex", SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.Save();
        Debug.Log("Saved scene " + SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadGame()
    {
        if(PlayerPrefs.HasKey("SceneIndex"))
            sceneIndex = PlayerPrefs.GetInt("SceneIndex");
    }

    public void LoadScene1()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadScene2()
    {
        SceneManager.LoadScene(2);
    }
}
