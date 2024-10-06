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

    // edward code
    private bool failed = false;

    // Start is called before the first frame update
    void Start()
    {
        // gameTimer = Manager;

        if (Manager.Instance.MiniGameTimeMax == 10)
        {
            moveSpeed = 0.4f;
        }
        else
        {
            moveSpeed = 0.4f + (0.05f * (10.0f - Manager.Instance.MiniGameTimeMax));
        }
        
        slasher.transform.position = slasherPos;
        noiseSpeed = moveSpeed / 3f;
        Debug.Log("Noise Speed: " + noiseSpeed);
        Debug.Log("Move Speed: " + moveSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        slasherPos.x += moveSpeed * Time.deltaTime;
        slasher.transform.position = slasherPos;
        if (slasherPos.x > 0.5f)
        {
            moveSpeed = 0.1f;
        }

        // gameTimer -= Time.deltaTime;
        noiseLevel.y += noiseSpeed * Time.deltaTime;

        noiseBar.transform.localScale = noiseLevel;

        //lose state
        if (noiseLevel.y > .4f)
        {
            if (!failed)
                Manager.Instance.MiniGameTime = 1.0f;

            failed = true;
            doors.sprite = open;
            noiseLevel.y = .4f;
            slasherPos.x = -.1f;
        }

        if (noiseLevel.y < 0)
        {
            noiseLevel.y = 0;
        }
        if (Input.GetKeyDown(KeyCode.Space) && !failed)
        {
            noiseLevel.y -= .03f;
        }

        //win state
        if (Manager.Instance.MiniGameTime < 0)
        {
            //insert game end here
            //slasherPos.x = 20;
            Manager.Instance.EndMiniGame(!failed, true);
            //gameObject.Destroy(gameObject);
        }
    }
}
