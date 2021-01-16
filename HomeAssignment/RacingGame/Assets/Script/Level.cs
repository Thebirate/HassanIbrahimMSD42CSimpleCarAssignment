using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("RacingGame");
    }

    public void LoadGameOver()
    {
        SceneManager.LoadScene("StartMenu1");
    }

    public void Quit()
    {
        print("quitting game");
        Application.Quit();
    }
}
