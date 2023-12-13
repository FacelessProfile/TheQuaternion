using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    public Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.right,6f);
        if (hit.collider!=null)
        {
            Debug.Log("aboba");
        }
    }
}
