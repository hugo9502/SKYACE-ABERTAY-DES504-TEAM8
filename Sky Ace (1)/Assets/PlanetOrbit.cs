using UnityEngine;
using System.Collections;

public class PlanetOrbit : MonoBehaviour
{

    GameObject planet;
    public Transform center;
    public Vector3 axis = Vector3.up;
    public Vector3 desiredPosition;
    public float radius;
    public float radiusSpeed = 0.5f;
    public float rotationSpeed;
    public bool isOrbiting;

    public float force;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isOrbiting)
        {
            Orbit();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Catapult();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Planet")
        {
            planet = other.gameObject;
            center = planet.transform;
            ReCentre();
        }
    }

    void ReCentre()
    {
        rb.velocity = Vector3.zero;
        center = planet.transform;
        //transform.position = (transform.position - center.position).normalized * radius + center.position;
        radius = planet.transform.localScale.x + 0.5f;
        rotationSpeed = 140f / planet.transform.localScale.x;
        isOrbiting = true;
        Debug.Log(planet.transform);
        Debug.Log(desiredPosition);
    }

    void Orbit()
    {
        transform.LookAt(planet.transform.position);
        transform.RotateAround(center.position, axis, rotationSpeed * Time.deltaTime);
        desiredPosition = (transform.position - center.position).normalized * radius + center.position;
        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * radiusSpeed);
    }

    void Catapult()
    {
        isOrbiting = false;
        force = rotationSpeed * 70f;
        if (transform.localRotation.y < 0f)
        {
            rb.AddRelativeForce(Vector3.up * force * Time.deltaTime);
        } else if (transform.localRotation.y > 0f)
        {
            rb.AddRelativeForce(-Vector3.up * force * Time.deltaTime);
        }
        
    }

}