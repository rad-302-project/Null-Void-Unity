using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AsteroidsController : MonoBehaviour
{
    public GameObject Asteroid;

    [Range(0, 100)]
    public int numberOfAsteroids;
    public int AsteroidLimit = 10;

	// Use this for initialization
	void Start ()
    {

        
    }
    private void Update()
    {
        if (numberOfAsteroids <= AsteroidLimit)
        {
            Instantiate(Asteroid, new Vector3(Random.Range(-2500, 2500), 0, Random.Range(-2500, 2500)), Quaternion.identity);

            numberOfAsteroids++;
        }
    }

}
