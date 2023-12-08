using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractivePlate : MonoBehaviour
{
    public MovingPlatform movingPlatform;

    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Box")) movingPlatform.isActive = true;
        else movingPlatform.isActive = false;
    }
}
