using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    //bool for if the player is grounded
    private bool grounded = true;

    //audio reference
    AudioSource audioSource;

    //force applied when the player jumps
    [SerializeField]
    float jumpAmount = 555;

    Rigidbody2D rigidBody;

    //grab the rigid body for this object
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rigidBody.AddForce(Vector2.up * jumpAmount);
            grounded = false;
        }

        //win state
        if (Manager.Instance.MiniGameTime < 0)
        {
            Manager.Instance.EndMiniGame(true, true);
        }
    }

    //collision checks for the player, will run the endGame method held in the dino game manager
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Velociraptor"))
        {
            Manager.Instance.EndMiniGame(false, false);
            // Play stinger here
            audioSource.Play();
        }
    }
}
