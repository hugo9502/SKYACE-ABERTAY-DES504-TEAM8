using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotion : MonoBehaviour {

    public AudioSource soundtrack;
    public float modifier = 0.25f;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(1))
        {
            Time.timeScale = modifier;
            soundtrack.pitch = modifier * 2f;
        } else
        {
            Time.timeScale = 1f;
            soundtrack.pitch = 1f;
        }
	}
}
