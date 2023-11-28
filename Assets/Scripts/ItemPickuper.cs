using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemPickuper : MonoBehaviour
{
    public bool isBeingHeld = false;
    public GameObject Player;
    public float HighAbove;
    public float xTranslate;
    public float MaxDistance = 5f;

    private PlayerMovementNew playerMovement;
    private Rigidbody2D rb;

    void Start()
    {
        playerMovement = new PlayerMovementNew();
        playerMovement.AT_ATControl.Lever.started += PickUp;
        playerMovement.AT_ATControl.Lever.canceled += Release;
        playerMovement.AT_ATControl.Enable();
        rb = GetComponent<Rigidbody2D>();

        rb.freezeRotation = true;
    }

    public void PickUp(InputAction.CallbackContext context)
    {
        isBeingHeld = true;
    }

    public void Release(InputAction.CallbackContext context)
    {
        isBeingHeld = false;
    }

    void Update()
    {
        if (isBeingHeld)
        {
            Vector2 playerPosition = Player.transform.position;

            float distance = Vector2.Distance(transform.position, playerPosition);

            if (distance < MaxDistance)
            {
                transform.position = new Vector2(playerPosition.x + xTranslate, playerPosition.y + HighAbove);
            }
        }
    }
}
