using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public float score = 200;
    private int scoreInt;

    float orbitMultiplier = 1f;

    public Text scoreText;
	
	// Update is called once per frame
	void Update () {
        // Assign the scoreInt value to the scoreText UI Text game object:
        scoreText.text = scoreInt.ToString();

        if (GameObject.FindGameObjectWithTag("Player").GetComponent<Ball>().levelComplete == false)
        {
            // Check if the player is orbiting a planet:
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<Ball>().isOrbiting)
            {
                // Increase the rate at which the score decreases the longer the play remains orbiting a single planet:
                orbitMultiplier += 0.5f * Time.deltaTime;
                // Decrease the score:
                score -= 1 * Time.deltaTime * orbitMultiplier;
            }
            else
            {
                // If the player is not orbiting a planet, return the multiplier to 1:
                orbitMultiplier = 1f;
                // Decrease the score:
                score -= 1 * Time.deltaTime;
            }
        }
        
        // Cast the score float to an int scoreInt:
        scoreInt = (int)score;


        // RESET:
		if (Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene("SampleScene3");
        }
	}
}
