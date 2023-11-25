using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickuper : MonoBehaviour
{
    public CatMovement ScriptReference;
    private bool isBeingHeld = false;

    void Start()
    {
 
    }



    public bool IsBeingHeld
    {
        get { return isBeingHeld; }
    }

    public void PickUp()
    {
        isBeingHeld = true;
    }

    public void Release()
    {
        isBeingHeld = false;
    }
}
