using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class FigureFable : MonoBehaviour
{
    [SerializeField] TMP_Text stopStartText;
    [SerializeField] TMP_Text keyPressText;
    [SerializeField] SpriteRenderer SpriteRenderer;
    [SerializeField] GameObject toy;
    float interval;
    bool redlight;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        // Set the variables to their initial states
        stopStartText.text = "PLAY DEAD!";
        redlight = true;
        SpriteRenderer.color = Color.red;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Decreases interval every frame
        interval -= Time.deltaTime;

        // When the interval is less than 0 change the red light and reset interval to a random float
        if (interval < 0)
        {
            redlight = !redlight;
            interval = Random.Range(1.5f, 2.0f);
            
            // Changes the logic of the redlight
            redLightGreenLight(redlight);
        }
       
       
    }

    void redLightGreenLight(bool redlight)
    {
        if (!redlight)
        {
            // Reset timer
            timer = 0;

            // Change text to arnold isnt looking and sprite to looking away
            stopStartText.text = "ARNOLD ISN'T LOOKING!";
            SpriteRenderer.color = Color.green;
            
            // Sets toy upright 
            toy.transform.rotation = Quaternion.Euler(Vector3.forward * 0);
        }
        else
        {
            // Change text to play dead and sprite to looking at screen
            stopStartText.text = "PLAY DEAD!";
            SpriteRenderer.color = Color.red;

            // Generate one of 5 different keys for the event
            int key = Random.Range(0, 5);
            string keyCode = " ";
            timer = 0;
            bool stop = true;
            
            while (stop)
            {
                // Increase the timer
                timer += Time.deltaTime;

                // When the timer goes above the value below and the player has not laid down, they fail
                if (timer > 20.0f && toy.transform.localRotation.z == 0)
                {
                    Debug.Log("FAIL");
                    //return;
                }
                
                switch (key)
                {
                    case 0:
                        keyPressText.text = "PRESS A";
                        keyCode = "a";
                        break;
                    case 1:
                        keyPressText.text = "PRESS S";
                        keyCode = "s";
                        break;
                    case 2:
                        keyPressText.text = "PRESS D";
                        keyCode = "d";
                        break;
                    case 3:
                        keyPressText.text = "PRESS W";
                        keyCode = "w";
                        break;
                    case 4:
                        keyPressText.text = "PRESS SPACE";
                        // When user presses space the toy lays down
                        keyCode = "space";
                        break;
                }
                
                if (Input.GetKeyDown(keyCode))
                {
                    toy.transform.rotation = Quaternion.Euler(Vector3.forward * 90);
                    keyPressText.text = " ";
                    stop = false;
                }
                
            }
        }
    }
}
