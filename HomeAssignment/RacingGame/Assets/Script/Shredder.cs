using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        Destroy(collision.gameObject);

        if (collision.gameObject.tag == "Respawn")
        {
            FindObjectOfType<GameSession>().AddToScore(0);
        }
        else
        {
            FindObjectOfType<GameSession>().AddToScore(5);
        }
        

    }



}
