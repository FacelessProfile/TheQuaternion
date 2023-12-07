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

    public GameObject AT_AT;
    public GameObject Camera;
    public GameObject playerSquare;

    public Vector3 originalScale;
    private Vector2 movement;
    private Rigidbody2D rb;

    public PlayerMovementNew playerMovement;
    public LevelManager levelManager;
    public ItemPickuper itemPickuper;
    private SaveManager saveManager;
    private AT_ATMovement At;

    public Animator anim;

    public List<string> Inventory = new List<string>() { };

    void Start()
    {
        playerMovement = new PlayerMovementNew();
        playerMovement.PlayerActionsInput.DebugMessage.started += PrintDebugMessage;
        playerMovement.PlayerActionsInput.MovingHorisontal.performed += Moving;
        playerMovement.PlayerActionsInput.MovingHorisontal.canceled += StopMoving;
        playerMovement.PlayerActionsInput.Jump.started += Jump;
        playerMovement.PlayerActionsInput.ToLobby.canceled += ToLobby;
        playerMovement.PlayerActionsInput.DeleteProgress.canceled += DeleteProgress;
        playerMovement.PlayerActionsInput.SitInRobot.started += SitInRobot;
        playerMovement.PlayerActionsInput.Enable();

        this.originalScale = gameObject.transform.localScale;

        At = FindObjectOfType<AT_ATMovement>();
        rb = GetComponent<Rigidbody2D>();
        anim = playerSquare.GetComponent<Animator>();
        saveManager = GetComponent<SaveManager>();

        At.catInAT = false;

    }
    void PrintDebugMessage(InputAction.CallbackContext context)
    {
        Debug.Log("всё ጅርጅጌርጅግርፖጅ!!!");
    }
    void FixedUpdate()
    {
        transform.Translate(movement * speed * Time.fixedDeltaTime);
    }

    void Update()
    {
        if (movement.x < 0) playerSquare.transform.rotation = Quaternion.Euler(0, 180, 0);
        if (movement.x > 0) playerSquare.transform.rotation = Quaternion.Euler(0, 0, 0);
        if (movement.x == 0) anim.SetBool("Move", false); 
        if (movement.x < 0 || movement.x > 0) anim.SetBool("Move", true);
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

    void SitInRobot(InputAction.CallbackContext context)
    {
        Debug.Log(At.inside);
        if (At.inside)
        {
            At.catInAT = true;
            playerMovement.PlayerActionsInput.Disable();
            transform.SetParent(AT_AT.transform, false);
            Camera.transform.SetParent(AT_AT.transform, false);
            gameObject.SetActive(false);
            At.rb.isKinematic = false;

            At.playerMovement.AT_ATControl.Enable();
        }
    }

    void OnTriggerStay2D(Collider2D other)
    { 
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
