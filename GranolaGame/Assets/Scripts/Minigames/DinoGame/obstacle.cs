using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacle : MonoBehaviour
{
    public ObstacleGenerator obstacleGenerator;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * obstacleGenerator.speed *  Time.deltaTime);

        if (transform.position.x < -15)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NextLine"))
        {
            Debug.Log("running");
            obstacleGenerator.RandomizeSpace();
        }
    }
}
