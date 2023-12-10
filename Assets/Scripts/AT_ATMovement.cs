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
    public bool isGrounded;
    public bool catInAT = false;
    public bool inside;


    public PlayerMovementNew playerMovement;
    public SaveManager saveManager;
    private CatMovement catMovement;
    public LevelManager levelManager;

    public Vector2 movement;
    private Vector2 CurrentPos;
    public Vector3 originalScale;

    public Rigidbody2D rb;
    public Animator anim;
    private SpriteRenderer spriteRenderer;
    
    public GameObject Player;
    public GameObject playerSquare;

    void Start()
    {
        catMovement = Player.GetComponent<CatMovement>();
        saveManager = GetComponent<SaveManager>();

        playerMovement = new PlayerMovementNew();
        playerMovement.AT_ATControl.MovingHorisontal.performed += Moving;
        playerMovement.AT_ATControl.MovingHorisontal.canceled += StopMoving;
        playerMovement.AT_ATControl.Jump.started += Jump;
        playerMovement.AT_ATControl.RobotExit.started += CatExit;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();


        this.originalScale = gameObject.transform.localScale;
    }
    void FixedUpdate()
    {
        transform.Translate(movement * speed * Time.fixedDeltaTime);
    }
    void Update()
    {
        /* if (movement.x < 0)
         {

             spriteRenderer.flipX = false;
         }
         if (movement.x > 0) {

             spriteRenderer.flipX = true;
         }
        */

        if (movement.x == 0)
        {
            anim.SetBool("MovingR", false);
            anim.SetBool("MovingL", false);
        }


        if (movement.x > 0) 
        {
            playerSquare.transform.rotation = Quaternion.Euler(0, 180, 0);
            anim.SetBool("MovingR", true);
            spriteRenderer.flipX = false;
        }
        else anim.SetBool("MovingR", false);


        if (movement.x < 0)
        {
            playerSquare.transform.rotation = Quaternion.Euler(0, 0, 0);
            anim.SetBool("MovingL", true);
            spriteRenderer.flipX = false;
        }
        else anim.SetBool("MovingL", false);


        if (catInAT) anim.SetBool("CatInAT", true);


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
        if (catInAT && isGrounded)
        {
            CurrentPos = transform.position;
            catInAT = false;
            playerMovement.AT_ATControl.Disable();
            catMovement.playerMovement.PlayerActionsInput.Enable();
            Player.SetActive(true);
            catMovement.virtualCamera.Follow = Player.transform;
            rb.simulated = false;
            Player.transform.parent = null;
            Player.transform.localScale = catMovement.originalScale;
            Player.transform.position = new Vector2(CurrentPos.x + Offset, CurrentPos.y + Offset);

        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") || catInAT) inside = true;
        if (other.CompareTag("Ground")) isGrounded = true;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bounds")) ResetPosition();
        if (other.CompareTag("FinishLine"))
        {
            saveManager.SaveData($"Level_{levelManager.levelCount}", "Passed");
            levelManager.LevelLoad(levelManager.levelCount);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground")) isGrounded = false;
        if (other.CompareTag("Player"))
        {
            inside = false;
            Debug.Log("вышел"); 
        }
    }

    void ResetPosition()
    {
        transform.position = new Vector2(0f, 0f);
        rb.velocity = Vector2.zero;
        levelManager.LevelLoad(levelManager.levelCount);
    }
}