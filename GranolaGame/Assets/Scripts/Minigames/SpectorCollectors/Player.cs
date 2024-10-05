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
    float speed;  // movement speed amount
    
    [SerializeField]
    float jump;  // jump speed amount

    [SerializeField]  // vacuum from player
    GameObject vacuum;


    private float elapsedTime = 0.0f;
    private float cooldown = 0.2f;


    private bool isFacingRight = true;  // which way is player facing bool
    private bool isJumping;  // is player jumping bool
    private float move; // -1 if going left, 0 if not moving, 1 if going right



    // Update Method
    void Update()
    {
        elapsedTime += Time.deltaTime;

        move = Input.GetAxis("Horizontal");  // get which way player is moving

        Flip();  // flips player

        rb.velocity = new Vector2(speed * move, rb.velocity.y);  // get velocity vector

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

        // if cooldown has passed you have the option to jump
        if (elapsedTime > cooldown)
        {
            // add force if you jump
            if (Input.GetButtonDown("Jump") && isJumping == false)
            {
                rb.AddForce(new Vector2(rb.velocity.x, jump));

                isJumping = true;

                elapsedTime = 0.0f;
            }
        }
    }



    // if colliding isJumping is false
    private void OnCollisionEnter2D(Collision2D other)
    {
        isJumping = false;
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
