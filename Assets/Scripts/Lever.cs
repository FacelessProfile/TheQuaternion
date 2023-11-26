using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Lever : MonoBehaviour
{
    public bool StateFlag;
    public bool Inside;
    public GameObject textPrefab;
    private GameObject newText;
    public float heightAbove = 15f;
    private Vector2 newPosition;
    private float canvasScale = 36f;




    private PlayerMovementNew playerMovement;

    void Start()
    {
        
        playerMovement = new PlayerMovementNew();
        playerMovement.ABOBA.Lever.started += LeverState;
        playerMovement.ABOBA.Enable();

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && newText == null)
        {
            Debug.Log("Instantiating text");
            Vector2 newPosition = new Vector2(transform.position.x * canvasScale, transform.position.y * canvasScale + heightAbove* canvasScale);
            Canvas canvas = FindObjectOfType<Canvas>();
            newText = Instantiate(textPrefab, newPosition, Quaternion.identity);
            newText.transform.SetParent(canvas.transform, false);
            Inside = true;
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
            Inside = false;
        }
    }

  

    void LeverState(InputAction.CallbackContext context)
    {
        Debug.Log("EBAT");
        if (!StateFlag && Inside)
        {
            StateFlag = true;
        }
        else if (Inside)
        {
            StateFlag = false;
        }
    }
}


