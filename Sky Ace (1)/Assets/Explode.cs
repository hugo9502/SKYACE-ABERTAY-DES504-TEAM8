using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour {

    public GameObject explosion;
	
	// Update is called once per frame
	void Update () {
		if (GetComponent<Ball>().planetStats.isGoal)
        {
            ExplodeBall();
        }
	}

    void ExplodeBall()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
