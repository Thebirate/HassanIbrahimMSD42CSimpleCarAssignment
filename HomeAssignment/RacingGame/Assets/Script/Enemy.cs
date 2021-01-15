using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   
    [SerializeField] float shotCounter; 
    [SerializeField] float minTimeBetweenShots = 0.2f; 
    [SerializeField] float maxTimeBetweenShots = 3f;

    [SerializeField] GameObject enemyLaserPrefab;
    [SerializeField] float enemyLaserSpeed = 20f;

   
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
        
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();

        if (!damageDealer) //checking if the damageDealer variable is empty/null
        {
            return; 
        }

        
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

            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots); // shotCounter needs
            //to be recalculated so that the enemy will shoot the NEXT laser once the new timer is up
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
