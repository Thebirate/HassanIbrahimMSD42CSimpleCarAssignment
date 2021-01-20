using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonShootingEnemy : MonoBehaviour
{
  
    [SerializeField] int scoreValue = 5;
    [SerializeField] int health = 10;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;

    
    [SerializeField] private GameObject deathVFX;
    [SerializeField] private AudioClip EnemyDeathSound;
    [SerializeField] [Range(0, 1)] private float enemyDeathSoundVolume = 0.75f;

    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }


    

    private void OnTriggerEnter2D(Collider2D collision)
    {

        AudioSource.PlayClipAtPoint(EnemyDeathSound, Camera.main.transform.position, enemyDeathSoundVolume);
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        GameObject explosion = Instantiate(deathVFX, transform.position, Quaternion.identity);

        Destroy(explosion, 1f);
        if (!damageDealer)
        {
            return;
        }
       

    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage(); // health = health - damagedDealer.GetDamage();
        // A -= B; => A = A - B;
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (FindObjectOfType<Shredder>().transform.position == gameObject.transform.position)
        {

        }
        
        Destroy(gameObject);
    }

   



}
