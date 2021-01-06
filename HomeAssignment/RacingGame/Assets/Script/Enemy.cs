using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 100;
    [SerializeField] float shotCounter; // random time for the enemy to wait before shooting the next 
    //laser. The time will be reduced every frame so that once the time is up, the enemy can shoot the
    //laser.
    [SerializeField] float minTimeBetweenShots = 0.2f; // a range for the random number is required and
    //we need the shortest possible time to wait to shoot and the longest possible time to wait to shoot.
    [SerializeField] float maxTimeBetweenShots = 3f;

    [SerializeField] GameObject enemyLaserPrefab;
    [SerializeField] float enemyLaserSpeed = 20f;

    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }
    /* To handle collisions we can either use OnCollisionEnter2D or OnTriggerEnter2D (2D needs to be used if
     * the colliders are 2D). OnTriggerEnter2D needs to be used if at least one of the colliders is a 
     * trigger collider (has is trigger ticked).
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //We are retrieving the damage dealer of the current laser which hit the enemy since different
        //lasers CAN have different damage values
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();

        if (!damageDealer) //checking if the damageDealer variable is empty/null
        {
            return; // makes the method stop and return back, thus ProcessHit() will never be called IF
            // the damageDealer is empty.
        }

        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage(); // health = health - damagedDealer.GetDamage();
        // A -= B; => A = A - B;
        damageDealer.Hit();
        if (health <= 0)
        {
            Destroy(gameObject);
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
        GameObject enemyLaser = Instantiate(enemyLaserPrefab, transform.position, Quaternion.identity) as GameObject;
        //GameObject enemyLaserClone = Instantiate(enemyLaserPrefab, transform.position, Quaternion.Euler(0,0,180));
        enemyLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -enemyLaserSpeed);

        //enemyLaserClone.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -enemyLaserSpeed);
    }
}
