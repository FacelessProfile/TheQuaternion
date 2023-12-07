using System.Collections;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    public bool State;
    public float DoorSpeed = 2f;
    public float DoorDuration = 3f;
    public bool InMove;

    private Vector2 initialPosition;
    private Vector2 targetPosition;

    private Lever lever;
    public Rigidbody2D DoorRigidbody;

    void Start()
    {
        lever = FindObjectOfType<Lever>();
        initialPosition = DoorRigidbody.position;
        targetPosition = initialPosition + new Vector2(0f, 5f);
    }

    void Update()
    {
        if (lever != null)
        {
            State = lever.StateFlag;
        }
            if (lever != null && State && !InMove)
            {
                Debug.Log("BimBim");
                MoveDoor();
                InMove = true;
            }
            else
            {
                
            }
    }

    void MoveDoor()
    {
        StartCoroutine(LerpDoorPosition());
    }

    IEnumerator LerpDoorPosition()
    {
        float elapsedTime = 0f;

        while (elapsedTime < DoorDuration)
        {
            DoorRigidbody.position = Vector2.Lerp(initialPosition, targetPosition, elapsedTime / DoorDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        DoorRigidbody.position = targetPosition;
    }

    /*void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
        }
    }*/
}
