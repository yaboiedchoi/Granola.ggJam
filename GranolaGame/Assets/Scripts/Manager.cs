using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : Singleton<Manager>
{
    [SerializeField]
    int score = 0;
    int lives = 3;
    float time;
    float timeDecreaseMultiplier;
    float playbackSpeed;
    List<GameObject> listOfGames;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndMiniGame(bool win) {
        if (win) {
            score += 100;
        }
        else {
            lives--;
        }
    }
}
