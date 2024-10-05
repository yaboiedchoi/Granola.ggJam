using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shout : MonoBehaviour
{
    [SerializeField]
    private GameObject slasher;
    [SerializeField]
    private GameObject noiseBar;
    [SerializeField]
    private SpriteRenderer doors;
    [SerializeField]
    private Sprite open;

    private Vector3 slasherPos = new Vector3(-3f, 0, 0);


    float gameTimer;
    float moveSpeed;
    float noiseSpeed;

    Vector3 noiseLevel = new Vector3(0.06141f, .2f, 0.06141f);

    // Start is called before the first frame update
    void Start()
    {
        gameTimer = 10;

        if (gameTimer == 10)
        {
            moveSpeed = .002f;
        }
        else
        {
            moveSpeed = .002f * (10 - gameTimer);
        }
        
        slasher.transform.position = slasherPos;
        noiseSpeed = moveSpeed/10;
    }

    // Update is called once per frame
    void Update()
    {
        slasherPos.x += moveSpeed;
        slasher.transform.position = slasherPos;
        if (slasherPos.x > -1f)
        {
            moveSpeed = .001f;
        }

        gameTimer -= Time.deltaTime;
        noiseLevel.y += noiseSpeed;

        noiseBar.transform.localScale = noiseLevel;

        //lose state
        if (noiseLevel.y > .4f)
        {
            doors.sprite = open;
            noiseLevel.y = .4f;
            slasherPos.x = -.1f;
        }

        if (noiseLevel.y < 0)
        {
            noiseLevel.y = 0;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            noiseLevel.y -= .03f;
        }

        //win state
        if (gameTimer < 0)
        {
            //insert game end here
            slasherPos.x = 20;
        }
    }
}
