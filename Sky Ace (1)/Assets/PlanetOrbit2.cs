using UnityEngine;
using System.Collections;

public class PlanetOrbit2 : MonoBehaviour
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

        center = planet.transform;
        transform.position = (transform.position - center.position).normalized * radius + center.position;
        radius = planet.transform.localScale.x;
        rotationSpeed = 140f / planet.transform.localScale.x;

        transform.RotateAround(center.position, axis, rotationSpeed * Time.deltaTime);
        desiredPosition = (transform.position - center.position).normalized * radius + center.position;
        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * radiusSpeed);

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
        }
    }

    void Orbit()
    {
        
    }

    void Catapult()
    {
        isOrbiting = false;
        rb.AddRelativeForce(-Vector3.forward * force * Time.deltaTime);
    }

}