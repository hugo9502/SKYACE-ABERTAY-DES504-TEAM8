using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {

    Vector3 randomAxis;

    public PlanetStats planetStats;

    void Start()
    {
        // Assign a random axis for the planet to spin on:
        randomAxis = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
    }

    // Update is called once per frame
    void Update () {
        // Assign the scale according to the PlanetStats associated with this planet:
        transform.localScale = new Vector3(planetStats.size, planetStats.size, planetStats.size);

        if (planetStats.isGoal)
        {
            GoalRotation();
        }
        else
        {
            Rotation();
        }
	}

    void GoalRotation()
    {
        transform.Rotate(Vector3.forward * 45f * Time.deltaTime);
    }
    void Rotation()
    {
        transform.Rotate(randomAxis * 25f * Time.deltaTime);
    }
}
