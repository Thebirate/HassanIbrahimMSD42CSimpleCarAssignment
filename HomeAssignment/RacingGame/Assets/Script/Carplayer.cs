using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Carplayer : MonoBehaviour
{
    
    [SerializeField] float movementSpeed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] int playerHealth = 50;
    [SerializeField] private GameObject deathVFX;
    [SerializeField] private AudioClip playerDeathSound;
    [SerializeField] [Range(0, 1)] private float playerDeathSoundVolume = 0.75f;
    float xMin;
    float xMax;
    float yMin;
    float yMax;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
    }
    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);

        transform.position = new Vector3(newXPos,transform.position.y);


    }
    private void SetUpMoveBoundaries()
    {
        //setup the boundaries of the movement according to the camera
        Camera gameCamera = Camera.main; // fetching the main camera
        float padding = 0.5f;



        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;

    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        playerHealth -= damageDealer.GetDamage(); // health = health - damagedDealer.GetDamage();
        // A -= B; => A = A - B;
        damageDealer.Hit();

        if (playerHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        AudioSource.PlayClipAtPoint(playerDeathSound, Camera.main.transform.position, playerDeathSoundVolume);
        GameObject explosion = Instantiate(deathVFX, transform.position, Quaternion.identity);

        Destroy(explosion, 1f);
        Destroy(gameObject);
        FindObjectOfType<Level>().LoadGameOver();
    }

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

}
