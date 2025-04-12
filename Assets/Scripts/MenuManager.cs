using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private Games games;

    public void Beginner()
    {
        GameSettings.width = 9;
        GameSettings.height = 9;
        GameSettings.mineCount = 10;
        SceneManager.LoadSceneAsync(1);
    }

    public void Intermediate()
    {
        GameSettings.width = 16;
        GameSettings.height = 16;
        GameSettings.mineCount = 40;
        SceneManager.LoadSceneAsync(1);
    }


    public void Expert()
    {
        GameSettings.width = 30;
        GameSettings.height = 16;
        GameSettings.mineCount = 99;
        SceneManager.LoadSceneAsync(1);
    }


    public void Exit()
    {
        Application.Quit();
    }
}
