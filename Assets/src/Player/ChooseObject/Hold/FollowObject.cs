using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public static GameObject CurrentlyHeldObject;
    public Transform player;
    public float followDistance = 1.5f;
    public float hoverHeight = 0.25f;
    public float followSpeed = 5f;

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Rigidbody rb;

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        initialPosition = transform.position;
        initialRotation = transform.rotation;
        rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
        }
    }

    private void Update()
    {
        if (player != null)
        {
            Vector3 targetPosition = player.position + player.forward * followDistance + Vector3.up * hoverHeight;
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
            transform.rotation = initialRotation;

            if (Input.GetKeyDown(KeyCode.E))
            {
                ReleaseObject();
            }
        }
    }

    private void ReleaseObject()
    {
        if (rb != null)
        {
            rb.useGravity = true;
        }

        Destroy(this);
        CurrentlyHeldObject = null;
    }

    private void OnDestroy()
    {
        if (rb != null)
        {
            rb.useGravity = true;
        }
    }
}
