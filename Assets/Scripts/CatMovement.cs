using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using Cinemachine;

public class CatMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 550f;
    public float maxHorizontalSpeed = 8f;
    public int lifes = 9;
    public bool isGrounded;

    private float lastJumpTime;

    public GameObject AT_AT;
    public GameObject playerSquare;
    public CinemachineVirtualCamera virtualCamera;

    public Vector3 originalScale;
    private Vector2 movement;
    private Rigidbody2D rb;

    public PlayerMovementNew playerMovement;
    public LevelManager levelManager;
    public ItemPickuper itemPickuper;
    public SaveManager saveManager;
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
        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();;

        At.catInAT = false;
        At.saveManager = saveManager;

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
        if (!isGrounded) anim.SetBool("Jump", true);
        if (isGrounded) anim.SetBool("Jump", false);

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
        LayerMask groundLayer = LayerMask.GetMask("Ground");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 0.9f, groundLayer);

        if (hit.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bounds")) ResetPosition();
        if (other.CompareTag("FinishLine"))
        {
            saveManager.SaveData($"Level_{levelManager.levelCount-1}", "Passed");
            levelManager.LevelLoad(levelManager.levelCount,true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
    }

    void ResetPosition()
    {
        levelManager.LevelLoad(levelManager.levelCount,false);
        transform.position = new Vector2(0f, 0f);
        rb.velocity = Vector2.zero;
        lifes--;
    }
    public void PickUpUpgrade(string Item)
    {
        Inventory.Add(Item);
    }

    public void SitInRobot(InputAction.CallbackContext context)
    {
        Debug.Log(At.inside);
        if (At.inside)
        {
            At.catInAT = true;
            virtualCamera.Follow = AT_AT.transform;
            playerMovement.PlayerActionsInput.Disable();
            transform.SetParent(AT_AT.transform, false);
            gameObject.SetActive(false);
            At.rb.simulated = true;
            At.playerMovement.AT_ATControl.Enable();
        }
    }

    void OnTriggerStay2D(Collider2D other)
    { 
        //if (other.CompareTag("Ground")) isGrounded = true;
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
