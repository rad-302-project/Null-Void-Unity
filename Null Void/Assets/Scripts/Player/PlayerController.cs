using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //player inputs
    public int moveSpeed, rotateSpeed;
    public Text overallScoreDisplay;
    public Transform respawnLocation, playerPosition, firePosition;  
    Rigidbody2D playerBody;  
     
    //projectile and sounds
    public GameObject bullet;
    public AudioClip hitSound;
    public AudioClip shotSound;
    private AudioSource source;
    public int Ammo = 5;
    bool canShoot = true;
    bool hasHit = true;
    int numOfShots;
    public float delayInSeconds;

    //volume for projectile
    private float volLowRange = .5f;
    private float volHighRange = 1.0f;

    //health
    public static float startHealth = 100;

    void Start()
    {
        // the rigidboody component of the player
        playerBody = GetComponent<Rigidbody2D>();

        // the transform of the player
        playerPosition = GetComponent<Transform>();

        //animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        HandleMovement();
        HandleAnimations();
        Fire();
        if (AsteroidTumbler.health <= 0)
        {
            Destroy(this.gameObject);
            Time.timeScale = 0;
        }
        
    }

    void HandleMovement()
    {
        // Forward
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.right * moveSpeed * Time.deltaTime * 60;
        }

        // Rotate Left
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, -moveSpeed * Time.deltaTime * rotateSpeed, 0);
        }

        // Rotate Right
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, moveSpeed * Time.deltaTime * rotateSpeed, 0);
        }
    }

    void HandleAnimations()
    {
        //animator.SetBool("Moving", moving); May or may not see use for this project. 
    }

    void Fire()
    {
        // if the mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            if (canShoot)
            {
                Instantiate(bullet, firePosition.position, firePosition.rotation);
                float volume = Random.Range(volLowRange, volHighRange);
                source.PlayOneShot(shotSound, volume);
            }
            canShoot = false;
            StartCoroutine(ShootDelay());
        }

        if (hasHit)
        {
            float volume = Random.Range(volLowRange, volHighRange);
            source.PlayOneShot(hitSound, volume);
        }
        
        
    }
    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);
        canShoot = true;
    }




}