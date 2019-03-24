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

    void Start()
    {
        // the rigidboody component of the player
        playerBody = GetComponent<Rigidbody2D>();

        // the transform of the player
        playerPosition = GetComponent<Transform>();
       
        //animator = GetComponent<Animator>();

       
    }

    void Update()
    {
        HandleMovement();
        HandleAnimations();
        Fire();

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
            }
            canShoot = false;
            StartCoroutine(ShootDelay());
        }
        
        
    }
    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);
        canShoot = true;
    }
}