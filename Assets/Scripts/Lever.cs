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
    private AT_ATMovement At;


    void Start()
    {
        playerMovement = new PlayerMovementNew();
        playerMovement.AT_ATControl.Lever.started += LeverState;

        At = FindObjectOfType<AT_ATMovement>();

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && newText == null && At.catInAT)
        {
            Debug.Log("ገሮንጅግይ3ፒፍዉፒብ");
            Vector2 newPosition = new Vector2(transform.position.x * canvasScale, transform.position.y * canvasScale + heightAbove* canvasScale);
            Canvas canvas = FindObjectOfType<Canvas>();
            newText = Instantiate(textPrefab, newPosition, Quaternion.identity);
            newText.transform.SetParent(canvas.transform, false);
            Inside = true;
        if (At.inside)
        {
            playerMovement.AT_ATControl.Enable();
        }
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
        Debug.Log("ርትግ0[ኢጅት34ቅ  ኢግህፕርሁ");
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


