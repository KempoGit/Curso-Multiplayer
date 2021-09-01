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

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.AddPlayer(this);
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, 0.2f, whatIsGround);

        theRB.velocity = new Vector2(velocity * moveSpeed, theRB.velocity.y);
    }

    private void FixedUpdate()
    {
        if(theRB.velocity.x < 0)
        {
            this.transform.localScale = new Vector3(-1f, 1f, 1f);
        } else if (theRB.velocity.x > 0)
        {
            this.transform.localScale = Vector3.one;
        }
    }

    private void LateUpdate()
    {
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("ySpeed", theRB.velocity.y);
        anim.SetFloat("speed", Mathf.Abs(theRB.velocity.x));
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

    public void Attack(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            anim.SetTrigger("attack");
        }
    }
}
