using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class BackFromThePast : MonoBehaviour
{
    [SerializeField] GameObject car;
    [SerializeField] TMP_Text text;
    [SerializeField] GameObject obstacle;
    [SerializeField] float speed;
    GameObject[,] obstacles;
    string position;
    int rand;

    // Start is called before the first frame update
    void Start()
    {
        position = "middle";
        obstacles = new GameObject[2,2];
        Manager.Instance.PlayLoop("Back to the Past/Back to the Past Loop");
    }

    // Update is called once per frame
    void Update()
    {
        // Handles user input and moves car side to side
        CarMovement();

        // Instantiates the object array
        if (obstacles[1,1] == null)
        {
            InstantiateObjects();
        }

        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                if (obstacles[i, j] == null)
                {
                    break;
                }
                obstacles[i,j].transform.position -= Vector3.up * speed * Time.deltaTime;
                if (obstacles[i,j].transform.position.y < -8)
                {
                    Destroy(obstacles[i,j]);
                    obstacles[i,j] = null;
                }
            }
        }

        if (Manager.Instance.MiniGameTime <= 0)
        {
            Debug.Log("WIN!!");
            Manager.Instance.EndMiniGame(true, true);
        }
    }

    void InstantiateObjects()
    {
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                rand = Random.Range(-1, 2);
                Vector3 randomVector = new Vector3(rand * 3, i * 15 + 11, 0);
                obstacles[i,j] = Instantiate(obstacle, randomVector, Quaternion.identity, this.transform);
            }


        }
    }

    void CarMovement()
    {
        if (Input.GetKeyDown(KeyCode.A) && position != "left")
        {
            car.transform.position -= new Vector3(3, 0, 0);

            if (position == "middle")
            {
                position = "left";
            }
            else
            {
                position = "middle";
            }
        }
        if (Input.GetKeyDown(KeyCode.D) && position != "right")
        {
            car.transform.position += new Vector3(3, 0, 0);

            if (position == "middle")
            {
                position = "right";
            }
            else
            {
                position = "middle";
            }
        }
    }
}
