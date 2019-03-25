using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AsteroidTumbler : MonoBehaviour {

    public float tumbleSpeed = 1f;
    Rigidbody rb;   
    int aPoints = 20;
    public int score;

    // Use this for initialization
    void Start ()    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.forward);
        rb.angularVelocity = Random.insideUnitSphere * tumbleSpeed;
	}
   
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            // Injure Player
            
            // Remove Asteroid from game
            Destroy(this.gameObject);
        }
        if (collision.tag == "Bullet")
        {


            // Remove the asteroid from the game
            Destroy(this.gameObject);

            //Remove the Bullet
            Destroy(collision.gameObject);
        }
    }
    

}
