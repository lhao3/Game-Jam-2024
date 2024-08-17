using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float jumpForce = 2f;
    private float horizontalMovement;
    private Rigidbody2D rb2D;
    private bool hasJumped = false;
    private bool isGrounded = false; 
    public Collider2D floorCollider;
    public ContactFilter2D floorFilter;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        isGrounded = floorCollider.IsTouching(floorFilter);

        if(!hasJumped && Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            hasJumped = true; 
        }
    }

    private void FixedUpdate()
    {
        rb2D.velocity = new Vector2(horizontalMovement * movementSpeed, rb2D.velocity.y);
        if (hasJumped)
        {
            hasJumped = false;
            rb2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
