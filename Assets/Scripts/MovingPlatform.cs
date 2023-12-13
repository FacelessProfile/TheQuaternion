using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public bool isActive = false;
    public float moveSpeed = 5f;
    public float maxDistance = 5f;
    public int direction;

    private Transform playerOnPlatform;
    private bool isPlayerOnPlatform;
    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void FixedUpdate()
    {
        if (isActive)
        {
            float movement = moveSpeed * Time.fixedDeltaTime * direction;

            float newPositionX = Mathf.Clamp(transform.position.x + movement, initialPosition.x - maxDistance, initialPosition.x + maxDistance);
            transform.position = new Vector3(newPositionX, transform.position.y, transform.position.z);

            if (isPlayerOnPlatform && playerOnPlatform != null)
            {
                playerOnPlatform.Translate(new Vector3(movement, 0, 0));
            }

            if (Mathf.Abs(transform.position.x - initialPosition.x) >= maxDistance)
            {
                direction = -direction;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnPlatform = other.transform;
            isPlayerOnPlatform = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnPlatform = null;
            isPlayerOnPlatform = false;
        }
    }
}

