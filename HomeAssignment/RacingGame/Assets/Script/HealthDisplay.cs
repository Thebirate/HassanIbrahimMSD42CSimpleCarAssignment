using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class HealthDisplay : MonoBehaviour
{
    Text healthText;
    Slider healthBar;
    GameObject healthBarFill;

    Carplayer player; // reference to the Player script so that we can access the player's health

    int maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        healthText = GetComponent<Text>();
        player = FindObjectOfType<Carplayer>();

        healthBar = FindObjectOfType<Slider>();
        maxHealth = player.GetHealth();
        healthBar.maxValue = maxHealth;

        
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = player.GetHealth().ToString();

        healthBar.value = player.GetHealth();

        
    }
}
