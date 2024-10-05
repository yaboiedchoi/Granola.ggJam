using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;

public class Player : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rb;

    [SerializeField]
    GameObject vacuum;

    private BoxCollider2D vacuumCollider;

    [SerializeField]
    GameObject ghost;

    private BoxCollider2D ghostCollider;

    [SerializeField]
    float speed;  // movement speed amount
    
    [SerializeField]
    float jump;  // jump speed amount

    private bool isFacingRight = true;  // which way is player facing bool

    private bool isJumping;  // is player jumping bool

    private float move; // -1 if going left, 0 if not moving, 1 if going right



    // Update Method
    void Update()
    {
        move = Input.GetAxis("Horizontal");  // get which way player is moving

        Flip();  // flips player

        rb.velocity = new Vector2(speed * move, rb.velocity.y);  // get velocity vector

        // add force if you jump
        if (Input.GetButtonDown("Jump") && isJumping == false)
        {
            rb.AddForce(new Vector2(rb.velocity.x, jump));
        }

        // if J is pressed vacuum shows
        if (Input.GetKeyDown(KeyCode.J))
        {
            vacuum.SetActive(true);
        }

        // if J is released vacuum disappears
        if (Input.GetKeyUp(KeyCode.J))
        {
            vacuum.SetActive(false);
        }

        VacuumHitGhost();  // check if vacuum collides with ghost, and if it does, ghost disappears

    }


    // if colliding isJumping is false
    private void OnCollisionEnter2D(Collision2D other)
    {
        isJumping = false;
    }


    // if not colliding isJumping is true
    private void OnCollisionExit2D(Collision2D other) 
    {
        isJumping = true;
    }


    // vacuum collding with ghost method
    private void VacuumHitGhost()
    {
        vacuumCollider = vacuum.GetComponent<BoxCollider2D>();
        ghostCollider = ghost.GetComponent<BoxCollider2D>();

        if (vacuumCollider.IsTouching(ghostCollider) || ghostCollider.IsTouching(vacuumCollider))
        {
            ghost.SetActive(false);
        }
    }


    // flips player method
    private void Flip()
    {
        Vector3 currentScale = transform.localScale;

        if (move < 0f && isFacingRight)
        {
            isFacingRight = false;

            currentScale.x *= -1;
            transform.localScale = currentScale;
        }
        else if (move > 0f && !isFacingRight)
        {
           isFacingRight = true;

           currentScale.x *= -1;
           transform.localScale = currentScale;
        }
        
        
    }


}
