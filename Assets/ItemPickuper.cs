using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickuper : MonoBehaviour
{
    public PlayerMovement ScriptReference;
    void Start()
    {
 
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player")  && Input.GetKey(KeyCode.F))
        {
            ScriptReference.PickUp(gameObject.name);
            Destroy(gameObject);
        }

    }
}
