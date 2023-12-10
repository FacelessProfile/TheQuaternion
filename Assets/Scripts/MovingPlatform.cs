using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public bool isActive = false;
    public float moveSpeed = 5f;
    public float moveDirection = -1f;
    public float maxDistance = 5f;
    public float currentDistance = 0f;
    public int direction;
    public Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        switch (direction)
        {
            case 0:
            if (isActive)
            {
                rb.velocity = new Vector2(moveDirection * moveSpeed, 0f);
                currentDistance += moveSpeed * Time.deltaTime;

                if (currentDistance >= maxDistance)
                {
                    currentDistance = 0f;
                    moveDirection = -moveDirection;
                }
            }
            else rb.velocity = new Vector2(0, 0);
                break;

            case 1:
                if (isActive)
                {
                    rb.velocity = new Vector2(0f, -moveDirection * moveSpeed);
                    currentDistance += moveSpeed * Time.deltaTime;

                    if (currentDistance >= maxDistance)
                    {
                        rb.velocity = new Vector2(0, 0);
                        currentDistance = 13f;
                    }
                }
                else
                {
                    rb.velocity = new Vector2(0f, moveDirection * moveSpeed);
                    currentDistance -= moveSpeed * Time.deltaTime;

                    if (currentDistance <= 0)
                    {
                        rb.velocity = new Vector2(0, 0);
                        currentDistance = 0f;
                    }
                }
                break;
            case 2:
                if (isActive)
                {
                    rb.velocity = new Vector2(0f, -moveDirection * moveSpeed);
                    currentDistance += moveSpeed * Time.deltaTime;

                    if (currentDistance >= maxDistance)
                    {
                        rb.velocity = new Vector2(0, 0);
                        currentDistance = 14f;
                    }
                }
                else
                {
                    rb.velocity = new Vector2(0f, moveDirection * moveSpeed);
                    currentDistance -= moveSpeed * Time.deltaTime;

                    if (currentDistance <= 0)
                    {
                        rb.velocity = new Vector2(0, 0);
                        currentDistance = 0f;
                    }
                }
                break;
        }
    }
}
