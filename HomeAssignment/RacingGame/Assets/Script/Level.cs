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
        FindObjectOfType<GameSession>().ResetGame();
    }

    public void LoadGameOver()
    {
        SceneManager.LoadScene("Over");
    }
    public void LoadWon()
    {
        SceneManager.LoadScene("Won");
    }
    public void Quit()
    {
        print("quitting game");
        Application.Quit();
    }
}
