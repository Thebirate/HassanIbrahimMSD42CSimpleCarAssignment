using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField] int health = 10;
    [SerializeField] float shotCounter; 
    [SerializeField] float minTimeBetweenShots = 0.2f; 
    [SerializeField] float maxTimeBetweenShots = 3f;

    [SerializeField] GameObject enemyLaserPrefab;
    [SerializeField] float enemyLaserSpeed = 20f;
    [SerializeField] private GameObject deathVFX;
    [SerializeField] private AudioClip EnemyDeathSound;
    [SerializeField] [Range(0, 1)] private float enemyDeathSoundVolume = 0.75f;

    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    
    void Update()
    {
        CountDownAndShoot();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(EnemyDeathSound, Camera.main.transform.position,enemyDeathSoundVolume);
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

        //GameObject explosion = Instantiate(deathVFX, transform.position, Quaternion.identity);

        //Destroy(explosion, 1f);
        Destroy(gameObject);
    }

    void CountDownAndShoot()
    {
        /* CountDownAndShoot is called in the Update (thus, every frame) and so if we reduce the time
         * taken for each frame from the shotCounter we are actually reducing the game time from the
         * shotCounter.
         */
        shotCounter -= Time.deltaTime;

        if (shotCounter <= 0)
        {
            // shooting the laser
            EnemyFire();

            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots); 
        }
    }

    void EnemyFire()
    {
        //GameObject enemyLaser = Instantiate(enemyLaserPrefab, transform.position, Quaternion.identity) as GameObject;
        GameObject enemyLaserClone = Instantiate(enemyLaserPrefab, transform.position, Quaternion.Euler(0,0,180));
        //enemyLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -enemyLaserSpeed);

        enemyLaserClone.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -enemyLaserSpeed);
    }
}
