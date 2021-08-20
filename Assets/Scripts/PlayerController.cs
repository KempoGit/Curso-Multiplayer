using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D theRB;
    public float moveSpeed, jumpForce;

    private float velocity;
    private bool isGrounded;

    public Transform groundCheckPoint;
    public LayerMask whatIsGround;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, 0.2f, whatIsGround);

        theRB.velocity = new Vector2(velocity * moveSpeed, theRB.velocity.y);
    }

    public void Move(InputAction.CallbackContext context)
    {
        velocity = context.ReadValue<Vector2>().x;
    }
    
    public void Jump(InputAction.CallbackContext context)
    {
        if(context.started && isGrounded)
        {
            theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
        }

        if(!isGrounded && context.canceled && theRB.velocity.y > 0f)
        {
            theRB.velocity = new Vector2(theRB.velocity.x, theRB.velocity.y * 0.5f);
        }
    }
}
