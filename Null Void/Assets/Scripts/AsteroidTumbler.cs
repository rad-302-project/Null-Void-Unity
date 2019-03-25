using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AsteroidTumbler : MonoBehaviour {

    public float tumbleSpeed = 1f;
    Rigidbody rb;   
    int aPoints = 20;
    public static int Score = 0; 
    public static float Health = 100;
    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.forward);
        rb.angularVelocity = Random.insideUnitSphere * tumbleSpeed;
	}

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            Health -= 10f;
            // Remove Asteroid from game
            Destroy(this.gameObject);

            
        }
        if (collision.tag == "Bullet")
        {
            Score += aPoints;
            // Remove the asteroid from the game
            Destroy(this.gameObject);

            //Remove the Bullet
            Destroy(collision.gameObject);
        }
    }
}