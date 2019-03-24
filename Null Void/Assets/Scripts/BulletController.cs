using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : PlayerController {

    public Vector2 speed;
    Rigidbody rb;
	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        rb.velocity = this.transform.TransformDirection(Vector3.right * speed);

    }
}
