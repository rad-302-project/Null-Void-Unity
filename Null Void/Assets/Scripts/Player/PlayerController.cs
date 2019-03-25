using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public int moveSpeed, rotateSpeed;
    public Text overallScoreDisplay;
    public Transform respawnLocation, playerPosition, firePosition;
    public int Ammo = 5;
    bool canShoot = true;
    int numOfShots;
    Rigidbody2D playerBody;
    public float delayInSeconds;
    //Animator animator;
    //Bullets   
    public GameObject bullet;
    public AudioClip shotSound;
    private AudioSource source;
    private float volLowRange = .5f;
    private float volHighRange = 1.0f;
    public static float startHealth = 100;
    public Image healthBar;

    UiController uiController;

    void Start()
    {
        uiController = GameObject.Find("Controller_Menu").GetComponent<UiController>();

        // the rigidboody component of the player
        playerBody = GetComponent<Rigidbody2D>();

        // the transform of the player
        playerPosition = GetComponent<Transform>();

        //animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }
    

    void Update()
    {
<<<<<<< HEAD
        
        if (!PauseMenu.GamePaused)
        {
            HandleMovement();
            HandleAnimations();
            Fire();

            if (AsteroidTumbler.health <= 0)
            {
                PauseMenu.GamePaused = true;
                Destroy(this.gameObject);
                Time.timeScale = 0;

            }
        }
       
        
=======
        HandleMovement();
        HandleAnimations();
        Fire();
        if (AsteroidTumbler.Health <= 0)
        {
            //Destroy(this.gameObject);
            uiController.LoadResultsScreen();
            Time.timeScale = 0;
        }     
>>>>>>> cd4c4d5a3c5e929900aa59952ddb9a704a5b8525
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
<<<<<<< HEAD
        
        
=======
            canShoot = false;
            StartCoroutine(ShootDelay());
        }              
>>>>>>> cd4c4d5a3c5e929900aa59952ddb9a704a5b8525
    }

    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);
        canShoot = true;
    }
}