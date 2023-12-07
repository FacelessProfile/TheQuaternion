using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class AT_ATMovement : MonoBehaviour
{
    public float Offset = 1f;
    public float speed = 10f;
    public float jumpForce = 1000f;
    public float maxHorizontalSpeed = 15f;
    private bool isGrounded;
    public bool catInAT = false;
    public bool inside;


    public PlayerMovementNew playerMovement;
    private SaveManager saveManager;
    private CatMovement catMovement;
    public LevelManager levelManager;

    public Vector2 movement;
    private Vector2 CurrentPos;
    public Vector3 originalScale;
    public Rigidbody2D rb;

    public GameObject Player;
    public GameObject playerSquare;

    void Start()
    {
        catMovement = Player.GetComponent<CatMovement>();
        saveManager = Player.GetComponent<SaveManager>();

        playerMovement = new PlayerMovementNew();
        playerMovement.AT_ATControl.MovingHorisontal.performed += Moving;
        playerMovement.AT_ATControl.MovingHorisontal.canceled += StopMoving;
        playerMovement.AT_ATControl.Jump.started += Jump;
        playerMovement.AT_ATControl.RobotExit.started += CatExit;

        rb = GetComponent<Rigidbody2D>();

        this.originalScale = gameObject.transform.localScale;
    }
    void FixedUpdate()
    {
        transform.Translate(movement * speed * Time.fixedDeltaTime);
    }
    void Update()
    {
        if (movement.x < 0) playerSquare.transform.rotation = Quaternion.Euler(0, 180, 0);
        if (movement.x > 0) playerSquare.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    void Moving(InputAction.CallbackContext context)
    {
        if (rb == null) return;
        movement = context.ReadValue<Vector2>();
        Debug.Log(playerMovement);
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
            Debug.Log(playerMovement);
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
    void CatExit(InputAction.CallbackContext context)
    {
        if (catInAT)
        {
            CurrentPos = transform.position;
            catInAT = false;
            Camera mainCamera = FindObjectOfType<Camera>();
            playerMovement.AT_ATControl.Disable();
            catMovement.playerMovement.PlayerActionsInput.Enable();
            Player.SetActive(true);
            rb.simulated = false;
            Player.transform.parent = null;
            Player.transform.localScale = catMovement.originalScale;
            Player.transform.position = new Vector2(CurrentPos.x + Offset, CurrentPos.y + Offset);
            mainCamera.transform.SetParent(Player.transform, false);
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") || catInAT) inside = true;
        else inside = false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground")) isGrounded = true;
        if (other.CompareTag("Bounds")) ResetPosition();
        if (other.CompareTag("FinishLine"))
        {
            saveManager.SaveData($"Level_{levelManager.levelCount++}", "Passed");
            levelManager.LevelLoad(levelManager.levelCount);
        }
    }
    void ResetPosition()
    {
        transform.position = new Vector2(0f, 0f);
        rb.velocity = Vector2.zero;
    }
}