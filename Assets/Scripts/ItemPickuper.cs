using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemPickuper : MonoBehaviour
{
    public bool isBeingHeld = false;
    public GameObject AT_AT;
    public GameObject hint;
    public float HighAbove;
    public float xTranslate;
    public float MaxDistance = 5f;

    private PlayerMovementNew playerMovement;
    private AT_ATMovement At;
    private Rigidbody2D rb;

    public void Start()
    {
        playerMovement = new PlayerMovementNew();
        playerMovement.AT_ATControl.HoldObject.started += PickUp;
        playerMovement.AT_ATControl.HoldObject.canceled += Release;
        playerMovement.AT_ATControl.Enable();
        rb = GetComponent<Rigidbody2D>();

        rb.freezeRotation = true;

        At = AT_AT.GetComponent<AT_ATMovement>();
    }

    public void PickUp(InputAction.CallbackContext context)
    {
        Debug.Log("поднять");
        if (At.catInAT) isBeingHeld = true;
    }

    public void Release(InputAction.CallbackContext context)
    {
        Debug.Log("опустить");
        if (At.catInAT) isBeingHeld = false;
    }

    void Update()
    {
        if (isBeingHeld)
        {
            if (hint != null) hint.SetActive(false);
            Vector2 playerPosition = AT_AT.transform.position;
            float distance = Vector2.Distance(rb.position, playerPosition);
            if (distance < MaxDistance)
            {
                if (At.playerSquare.transform.rotation.y == 0) xTranslate = 3f;
                if (At.playerSquare.transform.rotation.y > 0) xTranslate = -3f;
                rb.MovePosition(new Vector2(playerPosition.x + xTranslate, playerPosition.y + HighAbove));
                Debug.Log("Pick");
            }
        }
    }
}
