using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameSession : MonoBehaviour
{
    private int score = 0;
    public int Score()
    {
         return score; 

    }
    private void Awake()
    {
        SetUpSingleTon();
    }

    void SetUpSingleTon()
    {
        if (FindObjectsOfType< GameSession> ().Length > 1)
        {

            Destroy(gameObject);
        }
        else
        {

            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void AddToScore(int scoreValue)
    {
        score += scoreValue;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
