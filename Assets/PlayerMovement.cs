using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 550f;
    public float maxHorizontalSpeed = 8f; // ”становите значение максимальной горизонтальной скорости
    private bool isGrounded;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component is missing on the player object!");
        }
    }

    void Update()
    {
        if (rb == null) return;

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetKey(KeyCode.W) ? 0f : Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontalInput, 0f);

        // Ќормализуем вектор, чтобы обеспечить равномерную скорость во всех направлени€х
        movement.Normalize();

        // ѕримен€ем горизонтальное движение в любом состо€нии (прыжок или на земле)
        rb.velocity = new Vector2(movement.x * speed, rb.velocity.y);

        // ќграничиваем максимальную горизонтальную скорость
        float clampedHorizontalSpeed = Mathf.Clamp(rb.velocity.x, -maxHorizontalSpeed, maxHorizontalSpeed);
        rb.velocity = new Vector2(clampedHorizontalSpeed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        // «афиксируем угол поворота куба (player)
        transform.rotation = Quaternion.identity;
    }

    void FixedUpdate()
    {
        if (rb == null) return;

        if (!isGrounded && rb.velocity.y > 0)
        {
            rb.AddForce(Vector2.down * 2f, ForceMode2D.Force);
        }
    }

    void Jump()
    {
        if (rb == null) return;

        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce);
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
}
