using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityCheck : MonoBehaviour {

    public GameObject planet;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Ball ball = other.gameObject.GetComponent<Ball>();
            ball.planet = planet;
            ball.ReCentre();
            //if (!ball.teeOff)
            //{
            //    ball.ReCentre();
            //}
        }
    }
}
