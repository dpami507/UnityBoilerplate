using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public int moveSpeed = 5;
    public int jumpForce = 10;

    public bool isGrounded;

    public Transform groundCheck;
    public LayerMask groundLayer;
    
    private Rigidbody2D rb;
    public float extraGravity;
    
    public int yLevel;
    public Vector3 backPos;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 boxCastSize = new Vector2(GetComponent<BoxCollider2D>().size.x - .025f, 0.05f);
        
        isGrounded = Physics2D.BoxCast(groundCheck.position, boxCastSize, 0, Vector2.zero, 1, groundLayer);
        
        float x = Input.GetAxisRaw("Horizontal") * moveSpeed;

        transform.localScale = (rb.velocity.x < 0) ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);
        
        if (Input.GetButton("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        
        if (transform.position.y < yLevel)
        {
            transform.position = backPos;
            rb.velocity = Vector2.zero;
        }
        
        rb.velocity = new Vector2(x,rb.velocity.y);
        
        rb.AddForce(Vector2.down * extraGravity);
    }
}
