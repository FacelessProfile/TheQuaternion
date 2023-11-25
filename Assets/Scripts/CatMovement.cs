using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class CatMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 550f;
    public float maxHorizontalSpeed = 8f;
    private bool isGrounded;
    private bool isHold = true;
    bool isOn = true;

    private Vector2 movement;
    private Rigidbody2D rb;

    private PlayerMovementNew playerMovement;
    public LevelManager levelManager;
    private SaveManager saveManager;
    private ItemPickuper itemPickuper;

    public List<string> Inventory= new List<string>() { }; 

    void Start()
    {
        playerMovement = new PlayerMovementNew();
        playerMovement.PlayerActionsInput.DebugMessage.started += PrintDebugMessage;
        playerMovement.PlayerActionsInput.MovingHorisontal.performed += Moving;
        playerMovement.PlayerActionsInput.MovingHorisontal.canceled += StopMoving;
        playerMovement.PlayerActionsInput.Jump.started += Jump;
        playerMovement.PlayerActionsInput.PickObject.performed += TryPickUpObject;
        playerMovement.PlayerActionsInput.ToLobby.canceled += ToLobby;
        playerMovement.PlayerActionsInput.DeleteProgress.canceled += DeleteProgress;
        playerMovement.PlayerActionsInput.Enable();

        rb = GetComponent<Rigidbody2D>();
        saveManager = GetComponent<SaveManager>();
    }
    void PrintDebugMessage(InputAction.CallbackContext context)
    {
        Debug.Log("всё заебись!");
    }
    void FixedUpdate()
    {
        transform.Translate(movement * speed * Time.fixedDeltaTime);
        if (isHold) HoldObject();
    }

    void Update()
    {

    }

    void Moving(InputAction.CallbackContext context)
    {
        if (rb == null) return;
        movement = context.ReadValue<Vector2>();
        
    }

    void StopMoving(InputAction.CallbackContext context)
    {
        movement = Vector2.zero;
    }

    void Jump(InputAction.CallbackContext context)
    {
        if (rb == null) return;
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        if (other.CompareTag("Bounds"))
        {
            ResetPosition();
        }
        if (other.CompareTag("FinishLine"))
        {
            saveManager.SaveData($"Level_{levelManager.levelCount}", "Passed");
            levelManager.LevelLoad(++levelManager.levelCount);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void ResetPosition()
    {
        transform.position = new Vector2(0f, 0f);
        rb.velocity = Vector2.zero;
    }
    public void PickUpUpgrade(string Item)
    {
        Inventory.Add(Item);
    }

    private void TryPickUpObject(InputAction.CallbackContext context)
    {
        // Рейкаст (луч) для определения объекта перед игроком
        if (isOn)
        {
            Debug.Log("взял");
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 2f))
            {
                ItemPickuper holdableObject = hit.collider.GetComponent<ItemPickuper>();
                if (holdableObject != null)
                {
                    isOn = false;
                    itemPickuper = holdableObject;
                    itemPickuper.PickUp();
                }
            }
        }
        else if (!isOn)
        {
            Debug.Log("бросил");
            if (itemPickuper != null)
            {
                isOn = true;
                itemPickuper.Release();
                itemPickuper = null;
            }
        }
    }

    private void HoldObject()
    {
        if (itemPickuper != null && !itemPickuper.IsBeingHeld)
        {
            // перемещение объекта впереди игрока
            Vector3 holdPosition = transform.position + transform.forward * 2f;
            itemPickuper.transform.position = holdPosition;
        }
    }

    private void ToLobby(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene("Lobby");

    }
    private void DeleteProgress(InputAction.CallbackContext context)
    {
        PlayerPrefs.DeleteAll();
        levelManager.Progress = "Null";
        SceneManager.LoadScene("Lobby");
    }
}
