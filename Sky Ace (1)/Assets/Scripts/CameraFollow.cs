using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Vector3 offset;
    public float smoothTime;

    Transform planet;

    private Camera cam;

    public float currentZoom = 15f;
    float targetZoom = 15f;
    float minZoom = 5f;
    float maxZoom = 25f;

    float smooth;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update () {
        Follow();

        // The following code controls camera zooming with the scroll wheel:
        float scroll = Input.GetAxisRaw("Mouse ScrollWheel") * 10f;

        if (scroll != 0f)
        {
            targetZoom = Mathf.Clamp(targetZoom - scroll, minZoom, maxZoom);
        }

        currentZoom = Mathf.SmoothDamp(currentZoom, targetZoom, ref smooth, .15f);

        cam.orthographicSize = currentZoom;

	}

    void Follow()
    {
        planet = Ball.instance.planet.transform;
        Vector3 velocity = Vector3.zero;
        Vector3 targetPosition = planet.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
