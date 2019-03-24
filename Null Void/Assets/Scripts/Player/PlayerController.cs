using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public int moveSpeed, rotateSpeed;
    public Text overallScoreDisplay;
    public Transform respawnLocation, playerPosition;

    Rigidbody2D playerBody;
    //Animator animator;

    Transform firePosition;

    //Bullets
    public GameObject bullet;

    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        playerPosition = GetComponent<Transform>();
        //animator = GetComponent<Animator>();

        //gets the reference of the transform 
       // firePosition = transform.Find("firePosition");
    }

    void Update()
    {
        HandleMovement();
        HandleAnimations();

        if (Input.GetMouseButtonDown(0))
        {
            Fire();
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
        Instantiate(bullet);
    }
}