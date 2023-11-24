using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 550f;
    public float maxHorizontalSpeed = 8f; // Установите значение максимальной горизонтальной скорости
    private bool isGrounded;
    private Rigidbody2D rb;
    private SaveManager saveManager;
    public List<string> Inventory= new List<string>() { }; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        saveManager = GetComponent<SaveManager>();
    }

    void Update()
    {
        if (rb == null) return;

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetKey(KeyCode.W) ? 0f : Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontalInput, 0f);

        // Нормализуем вектор, чтобы обеспечить равномерную скорость во всех направлениях
        movement.Normalize();

        // Применяем горизонтальное движение в любом состоянии (прыжок или на земле)
        rb.velocity = new Vector2(movement.x * speed, rb.velocity.y);

        // Ограничиваем максимальную горизонтальную скорость
        float clampedHorizontalSpeed = Mathf.Clamp(rb.velocity.x, -maxHorizontalSpeed, maxHorizontalSpeed);
        rb.velocity = new Vector2(clampedHorizontalSpeed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
        
        

        // Зафиксируем угол поворота куба (player)
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
        if (other.CompareTag("FinishLine")) //когда игрок заходит в финиш лайн на уровне то его тепает в лобби хуета механика конечно но не ебу как это сделать в финиш скрипте, додумай пэжэ
        {
            saveManager.SaveData("Level_1", "Passed");
            SceneManager.LoadScene("Lobby");
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
    public void PickUp(string Item)
    {
        Inventory.Add(Item);
    }
}
