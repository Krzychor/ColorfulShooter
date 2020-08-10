using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    [SerializeField]
    private Transform objectToFollow;
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private float smoothValue;
    [SerializeField]
    private float borderX;
    private Vector3 offset;

    private Rigidbody2D playerRigidbody;
    private float camSizeBase;
    [SerializeField]
    private float camSizeSpeedMin = 1000;
    [SerializeField]
    private float camSizeSpeedMax = 2000;
    [SerializeField]
    private float camSizeMultiplier = 0.3f;

    void Start()
    {
        transform.position = new Vector3(objectToFollow.position.x, objectToFollow.position.y, -10);
        offset = transform.position - objectToFollow.position;
        cam = GetComponent<Camera>();
        camSizeBase = cam.orthographicSize;
        playerRigidbody = objectToFollow.gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        updateCameraSize();
    }

    void FixedUpdate ()
    {
        Vector3 objectPosition = cam.WorldToViewportPoint(objectToFollow.position);
        if (objectPosition.x > borderX + 0.5f || objectPosition.x < -borderX + 0.5f)
        {
            Vector3 destination = objectToFollow.position + offset;
            transform.position = Vector3.Lerp(transform.position, destination, smoothValue * Time.deltaTime);
        }
    }

    private void updateCameraSize()
    {
        if (Mathf.Abs(playerRigidbody.velocity.x) > camSizeSpeedMin &&
            Mathf.Abs(playerRigidbody.velocity.x) < camSizeSpeedMax)
        {
            cam.orthographicSize = camSizeBase + camSizeMultiplier * (Mathf.Abs(playerRigidbody.velocity.x) - camSizeSpeedMin);
        }
        else
        {
            cam.orthographicSize = Mathf.Abs(playerRigidbody.velocity.x) < camSizeSpeedMin ? camSizeBase : cam.orthographicSize;
        }
    }
}
