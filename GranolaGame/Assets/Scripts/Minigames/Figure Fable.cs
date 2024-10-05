using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FigureFable : MonoBehaviour
{
    [SerializeField] TMP_Text stopStartText;
    [SerializeField] SpriteRenderer SpriteRenderer;
    [SerializeField] GameObject toy;
    float interval;
    bool redlight;

    // Start is called before the first frame update
    void Start()
    {
        stopStartText.text = "MOVE!";
        redlight = false;
        SpriteRenderer.color = Color.green;
    }

    // Update is called once per frame
    void Update()
    {
        interval -= Time.deltaTime;

        if (interval < 0)
        {
            redlight = !redlight;
            interval = Random.Range(0.5f, 2.0f);
        }
       
        redLightGreenLight(redlight);
    }

    void redLightGreenLight(bool redlight)
    {
        if (!redlight)
        {
            stopStartText.text = "ARNOLD ISN'T LOOKING!";
            SpriteRenderer.color = Color.green;
            toy.transform.rotation = Quaternion.Euler(Vector3.forward);
        }
        else
        {
            stopStartText.text = "PLAY DEAD!";
            SpriteRenderer.color = Color.red;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                toy.transform.rotation = Quaternion.Euler(Vector3.forward * 90);
            }
        }
    }
}
