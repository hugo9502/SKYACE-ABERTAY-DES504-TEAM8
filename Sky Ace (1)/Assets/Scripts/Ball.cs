using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour {

    [HideInInspector] public GameObject planet;
    [HideInInspector] public PlanetStats planetStats;
    public GameObject startPlanet;

    public GameObject explosion;
    public AudioSource explosionSFX;

    private Transform center;
    private Vector3 axis = Vector3.up;

    public AudioSource swing;

    private LineRenderer lineRenderer;

    // public bool teeOff = true;
    // public Slider powerSlider;
    // float teeOffPower;
    [HideInInspector] public bool isOrbiting = false;
    [HideInInspector] public bool inOrbit = false;

    public float force;
    public float catapultForce;

    public bool levelComplete;

    private Rigidbody rb;

    // Use this for initialization
    void Start () {
        // Always default levelComplete to false at the start of a level:
        levelComplete = false;
        // Get a reference to the Rigidbody component:
        rb = GetComponent<Rigidbody>();
        // Get a reference to the LineRenderer component for aiming:
        lineRenderer = GetComponentInChildren<LineRenderer>();
        // Assign a starting velocity to the Rigidbody:
        rb.velocity = new Vector3(10f, 0f, 0f);
    }
	
	// Update is called once per frame
	void Update () {
        //if (teeOff)
        //{
        //    TeeOff();
        //}

        // Check if the player presses the left mouse button and is not currently at this level's goal planet. If true, call Catapult()
        if (Input.GetMouseButtonDown(0) && !planetStats.isGoal)
        {
            Catapult();
        } else if (Input.GetMouseButtonDown(0) && planetStats.isGoal) { // If they are currently at this level's goal planet, call Putt() instead
            Putt();
        }

        // Check if the player presses the right mouse button. If true, call Aim()
        if (Input.GetMouseButton(1))
        {
            Aim();
        } else if (Input.GetMouseButtonUp(1)) // When the player releases the right mouse button, disable the LineRenderer component:
        {
            lineRenderer.enabled = false;
        }
    }

    private void FixedUpdate()
    {
        // Check if isOrbiting is true. If true, call Orbit()
        if (isOrbiting)
        {
            Orbit();
        }
    }

    public void ReCentre()
    {
        // Assign the planetStats variable the values for the planetStats component on the current planet:
        planetStats = planet.GetComponent<Planet>().planetStats;
        // Set the orbit center to be the transform of the current planet:
        center = planet.transform;
        // Call CheckOrbit()
        CheckOrbit();
        // Set the force variable according to the speed of the current planet:
        force = force * planetStats.orbitSpeed;

        // Set the isOrbiting variable to true so that Orbit() will be called:
        isOrbiting = true;
    }

    void Orbit()
    {
        Vector3 r0 = (center.position - transform.position);

        float velocity1 = Mathf.Sqrt(force / r0.magnitude);
        //float velocity2 = Mathf.Sqrt(2.0f) * velocity1;
        //get the basic status of the ball and calculate the first cosmic velocity(which make the ball have the circle orbit around a planet)
        float angle = Mathf.Abs(Vector3.Angle(rb.velocity, r0));


        Vector3 orbitvelocity = Vector3.Cross(r0, Vector3.Cross(rb.velocity, r0));
        //change the velocity of the ball has the vertical direction with the position vector
        //also a condition of circle orbit

        //use the angle to evaluate if the ball is in the right position and start orbiting
        if (angle <= 100 && angle >= 80 && rb.velocity.magnitude != velocity1)
        {
            rb.velocity = orbitvelocity.normalized * velocity1;
            inOrbit = true;
        }

        rb.AddForce(r0.normalized * force * rb.mass / (r0.magnitude * r0.magnitude));

        Debug.Log(angle);
    }

    void Catapult()
    {
        isOrbiting = false;
        inOrbit = false;
        swing.Play();

        rb.AddForce(rb.velocity.normalized * catapultForce);
    }

    void CheckOrbit()
    {
        Vector3 v1 = rb.velocity;
        Vector3 v2 = planet.transform.position - transform.position;

        Vector3 v3 = Vector3.Cross(v1, v2);
    }

    //void TeeOff()
    //{
    //    powerSlider.value += 0.5f * Time.deltaTime;
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        teeOff = false;
    //        teeOffPower = powerSlider.value;
    //        ReCentre();
    //    }
    //}

    void Putt()
    {
        levelComplete = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Planet" && inOrbit==false)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            explosionSFX.Play();
            Destroy(this.gameObject);
        }
    }

    void Aim()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position + rb.velocity * 3f);

        lineRenderer.enabled = true;
    }
}
