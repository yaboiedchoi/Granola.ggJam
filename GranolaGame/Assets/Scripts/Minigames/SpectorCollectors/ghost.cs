using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rb;

    [SerializeField]
    float speed;  // movement speed amount

    public RaycastHit2D hitGround;
    public LayerMask groundLayers;
    public Transform groundCheck;

    private bool isFacingRight = true;  // which way is player facing bool


    
    // update method
    void Update()
    {
        hitGround = Physics2D.Raycast(groundCheck.position, -transform.up, 1f, groundLayers);
    }

    private void FixedUpdate()
    {
        Patrol();
    }


    // ghost patrol ground method
    private void Patrol()
    {
        if (hitGround.collider != false)  // if raycast hits, move the proper way
        {
            if (!isFacingRight)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
        }
        else  // else turn around
        {
            isFacingRight = !isFacingRight;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }
}
