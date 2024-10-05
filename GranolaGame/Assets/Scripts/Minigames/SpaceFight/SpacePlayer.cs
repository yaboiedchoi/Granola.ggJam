using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpacePlayer : MonoBehaviour
{
    Vector3 playerSpeed;

    // Start is called before the first frame update
    void Start()
    {
        playerSpeed = new Vector3(5f, 0f);
    }

    // Update is called once per frame
    void Update()
    {

        //movement
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= playerSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += playerSpeed * Time.deltaTime;
        }
        if (transform.position.x > 4f)
        {
            transform.position = new Vector3(4, 0);
        }
        if (transform.position.x < -4f)
        {
            transform.position = new Vector3(-4, 0);
        }
    }
}
