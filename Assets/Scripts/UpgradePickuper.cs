using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePickuper : MonoBehaviour
{
    public CatMovement ScriptReference;

    void Start()
    {
 
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ScriptReference.PickUpUpgrade(gameObject.name);
            Destroy(gameObject);
        }
    }
}
