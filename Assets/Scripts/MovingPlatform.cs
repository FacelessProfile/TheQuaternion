using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public bool isActive = false;
    public float moveSpeed = 5f;
    public float moveDirection = -1f;
    public float maxDistance = 5f;
    private float currentDistance = 0f;
    public Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isActive)
        {
            rb.velocity = new Vector2 (moveDirection * moveSpeed, 0f);
            currentDistance += moveSpeed * Time.deltaTime;

            if (currentDistance >= maxDistance)
            {
                currentDistance = 0f;
                moveDirection = -moveDirection;
            }   
        }
    }
}
