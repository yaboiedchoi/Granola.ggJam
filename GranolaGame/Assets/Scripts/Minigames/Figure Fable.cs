using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class FigureFable : MonoBehaviour
{
    [SerializeField] TMP_Text stopStartText;
    [SerializeField] TMP_Text keyPressText;
    [SerializeField] SpriteRenderer Background;
    [SerializeField] GameObject toy;
    float interval;
    [SerializeField] bool redlight;
    [SerializeField] float timer;

    // sprites
    [SerializeField] Sprite standng;
    [SerializeField] Sprite playDead;
    [SerializeField] Sprite noArnold;
    [SerializeField] Sprite yesArnold;

    string keyCode = " ";
    int key;

    // Start is called before the first frame update
    void Start()
    {
        // Set the variables to their initial states
        stopStartText.text = "ARNOLD ISN'T LOOKING";
        redlight = false;
        Background.sprite = noArnold;
        interval = 1f;
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
            RedLightGreenLight();
        }

        if (!redlight)
        {
            // Reset timer
            timer = 0;

            // Change text to arnold isnt looking and sprite to looking away
            stopStartText.text = "ARNOLD ISN'T LOOKING!";
            Background.sprite = noArnold;

            // Sets toy upright 
            toy.GetComponent<SpriteRenderer>().sprite = standng;
        }
        else
        {
            // Change text to play dead and sprite to looking at screen
            Background.sprite = yesArnold;
           
            // Increase the timer
            timer += Time.deltaTime;

            // When the timer goes above the value below and the player has not laid down, they fail
            if (timer > 1.0f && toy.transform.localRotation.z == 0)
            {
                Debug.Log("FAIL");
                stopStartText.text = " ";
                key = 6;
                return;
            }

            switch (key)
            {
                case 0:
                    stopStartText.text = "PRESS A";
                    keyCode = "a";
                    break;
                case 1:
                    stopStartText.text = "PRESS S";
                    keyCode = "s";
                    break;
                case 2:
                    stopStartText.text = "PRESS D";
                    keyCode = "d";
                    break;
                case 3:
                    stopStartText.text = "PRESS W";
                    keyCode = "w";
                    break;
                case 4:
                    stopStartText.text = "PRESS SPACE";
                    // When user presses space the toy lays down
                    keyCode = "space";
                    break;
                default:
                    break;
            }

            if (Input.GetKeyDown(keyCode))
            {
                toy.GetComponent <SpriteRenderer>().sprite = playDead;
                stopStartText.text = " ";
                key = 6;
            }
        }
    }

    void RedLightGreenLight()
    {
        // Generate one of 5 different keys for the event
        key = Random.Range(0, 5);
    }
}
