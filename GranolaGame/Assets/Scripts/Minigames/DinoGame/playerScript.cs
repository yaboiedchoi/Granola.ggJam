using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    //bool for if the player is grounded
    private int jumpCount = 2;

    //force applied when the player jumps
    [SerializeField]
    float jumpAmount = 555;

    Rigidbody2D rigidBody;

    //grab the rigid body for this object
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !(jumpCount == 0))
        {
            rigidBody.AddForce(Vector2.up * jumpAmount);
            jumpCount--;
        }
    }

    //collision checks for the player, will run the endGame method held in the dino game manager
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 2;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Velociraptor"))
        {
            Debug.Log("EndGame");
        }
    }
}
