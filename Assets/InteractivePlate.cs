using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractivePlate : MonoBehaviour
{
    public bool StateFlag;

    void Update()
    {
        Debug.Log(StateFlag);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Box"))
        {
            StateFlag = true;
        }
        else
        {
            StateFlag = false;
        }
    }
}
