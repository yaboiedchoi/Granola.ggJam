using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FigureFable : MonoBehaviour
{
    [SerializeField] TMP_Text stopStartText;
    float interval;
    bool redlight;
    float rand = Random.Range(1.5f, 4f);

    // Start is called before the first frame update
    void Start()
    {
        stopStartText.text = "MOVE!";
        redlight = false;
    }

    // Update is called once per frame
    void Update()
    {
        interval += Time.deltaTime;
        

        if (interval > rand)
        {
            redlight = !redlight;
            interval = 0;
        }
       

        redLightGreenLight(redlight);
    }

    void redLightGreenLight(bool redlight)
    {
        if (!redlight)
        {
            stopStartText.text = "MOVE!";
        }
        else
        {
            stopStartText.text = "PLAY DEAD!";
        }
    }
}
