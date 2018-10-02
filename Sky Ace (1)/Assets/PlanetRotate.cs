using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotate : MonoBehaviour {

    // Update is called once per frame
    void Update () {
        transform.Rotate(Vector3.up * 25f * Time.deltaTime);
	}
}
