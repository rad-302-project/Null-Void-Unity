using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsController : MonoBehaviour
{
    public GameObject Asteroid;

    Transform aTransform;

    [Range(0, 100)]
    public int numberOfAsteroids;
    public int AsteroidLimit = 10;
    
    public float RotateSpeed;

	// Use this for initialization
	void Start ()
    {
        aTransform = Asteroid.GetComponent<Transform>();
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        aTransform.Rotate(0, 6 * Time.deltaTime, 0);
        if (numberOfAsteroids <= AsteroidLimit)
        {
            Instantiate(Asteroid, new Vector3(Random.Range(-1000, 1000), 0, Random.Range(-1000, 1000)), Quaternion.identity);
            
            
            numberOfAsteroids++;
        }
        
	}
}
