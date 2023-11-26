using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemPickuper : MonoBehaviour
{
    public bool isBeingHeld = false;
    public GameObject Player;
    public float HighAbove;
    public float xTranslate;
    public float MaxDistance = 5f; // Максимальное расстояние, при котором Box следит за игроком

    private PlayerMovementNew playerMovement;
    private Rigidbody2D rb;

    void Start()
    {
        playerMovement = new PlayerMovementNew();
        playerMovement.ABOBA.Lever.started += PickUp;
        playerMovement.ABOBA.Lever.canceled += Release;
        playerMovement.ABOBA.Enable();
        rb = GetComponent<Rigidbody2D>();

        // Блокируем вращение объекта Box
        rb.freezeRotation = true;
    }

    public void PickUp(InputAction.CallbackContext context)
    {
        isBeingHeld = true;
    }

    public void Release(InputAction.CallbackContext context)
    {
        isBeingHeld = false;
    }

    void Update()
    {
        if (isBeingHeld)
        {
            // Получаем позицию игрока
            Vector2 playerPosition = Player.transform.position;

            // Проверяем расстояние между игроком и Box
            float distance = Vector2.Distance(transform.position, playerPosition);

            // Если расстояние меньше максимального значения, Box следит за игроком
            if (distance < MaxDistance)
            {
                // Устанавливаем позицию Box над игроком
                transform.position = new Vector2(playerPosition.x + xTranslate, playerPosition.y + HighAbove);
            }
        }
    }
}
