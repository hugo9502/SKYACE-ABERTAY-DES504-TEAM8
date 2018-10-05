using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    public float score = 200;

    float orbitMultiplier;

    public Text scoreText;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance == !this)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update () {
        UpdateScore();

        // RESET:
		if (Input.GetKeyDown("r"))
        {
            Restart();
        }
	}

    void UpdateScore()
    {
        orbitMultiplier = Ball.instance.planetStats.scoreMultiplier;

        if (Ball.instance.levelComplete == false)
        {
            // Check if the player is orbiting a planet:
            if (Ball.instance.isOrbiting)
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

            // Assign the score (cast to int) value to the scoreText UI Text game object:
            scoreText.text = ((int)score).ToString();
        }
    }

    void Restart()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
