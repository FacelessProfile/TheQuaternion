using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Lever : MonoBehaviour
{
    public bool StateFlag;
    public bool AlreadyActive;
    public GameObject textPrefab;
    private GameObject newText;

    private PlayerMovementNew playerMovement;
    

    void Start()
    {
        playerMovement.PlayerActionsInput.LeverActivate.started += LeverState;

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && newText == null)
        {
            Debug.Log("Instantiating text");
            newText = Instantiate(textPrefab, transform.position, Quaternion.identity);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (newText != null)
            {
                Destroy(newText);
                newText = null;
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
    }
    
    void LeverState(InputAction.CallbackContext context)
    {

            if (!AlreadyActive)
            {
                StateFlag = true;
                AlreadyActive = true;
            }

            if (AlreadyActive)
            {
                StateFlag = false;
                AlreadyActive = true;
            }
        
    }
}

