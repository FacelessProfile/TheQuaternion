using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishScript : MonoBehaviour
{
    public Rigidbody2D liftRigidbody;
    public GameObject FinishLine;
    public float liftSpeed = 2f;
    public float liftDuration = 3f;

    private Vector2 initialPosition;
    private Vector2 targetPosition;

    void Start()
    {
        initialPosition = liftRigidbody.position;
        targetPosition = initialPosition - new Vector2(0f, 5f); // Перемещение на 5 единиц вниз
    }



    void MoveLift()
    {
 
        Vector2 velocity = new Vector2(0f, -liftSpeed);
        liftRigidbody.velocity = velocity;
        StartCoroutine(StopLiftAfterDuration(velocity));
    }

    IEnumerator StopLiftAfterDuration(Vector2 velocity)
    {
        yield return new WaitForSeconds(liftDuration);
        liftRigidbody.velocity = Vector2.zero;
    }

    void OnTriggerEnter2D(Collider2D other)
    { 
        if (other.CompareTag("Player"))
        {
            MoveLift();
        }
    }
}


