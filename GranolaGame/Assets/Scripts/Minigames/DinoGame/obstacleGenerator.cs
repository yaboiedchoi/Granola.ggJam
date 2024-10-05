using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public GameObject obstacle;

    public float speed = 12;

    [SerializeField]
    float speedMult;

    //starts game by generating an obstacle
    private void Awake()
    {
        GenerateObstacle();
    }

    //creates a random amount of space between each obstacle
    public void RandomizeSpace()
    {
        float random = Random.Range(0.1f, 2.1f);
        Invoke("GenerateObstacle", random);
    }

    //generates each obstacle from a prefab
    private void GenerateObstacle()
    {
        GameObject obstacleInstance = Instantiate(obstacle, transform.position, transform.rotation);
        obstacleInstance.GetComponent<obstacle>().obstacleGenerator = this;
    }
}
