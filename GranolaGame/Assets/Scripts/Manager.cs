using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public enum GameState {
    MiniGame,
    VictoryStinger,
    DefeatStinger,
    GameOver
}
// public class Manager : Singleton<Manager>

public class Manager : MonoBehaviour
{
    // singleton
    private static Manager _instance;

    public static Manager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }
    
    // fields
    [SerializeField] private int score = 0;
    [SerializeField] private int lives = 3;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject heart;
    private List<GameObject> hearts = new List<GameObject>();
    
    [SerializeField] private float timeDecreaseMultiplier = 0.9f;
    // [SerializeField] private float playbackSpeed;
    [SerializeField] private GameState gameState;
    [SerializeField] private List<GameObject> listOfGames;
    [SerializeField] private GameObject currentGame;

    // timers
    [SerializeField] private float miniGameTime;
    [SerializeField] private float miniGameTimeMax = 10.0f;

    // used for game over screen as well
    [SerializeField] private float stingerTime;
    [SerializeField] private float stingerTimeMax = 2.0f;

    // game state info
    [SerializeField] private bool win;
    [SerializeField] private int roundNumber = 0;
    [SerializeField] private int currentGameIndex;
    [SerializeField] private int previousGameIndex = -1;
    [SerializeField] private int previousGameIndex2 = -1;
    [SerializeField] private AudioSource oneshotPlayer;
    [SerializeField] private AudioSource loopPlayer;

    // properties
    public float MiniGameTime {
        get { return miniGameTime; }
        set { miniGameTime = value; }
    }

    public float MiniGameTimeMax {
        get { return miniGameTimeMax; }
        set { miniGameTimeMax = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        gameState = GameState.MiniGame;
        miniGameTime = miniGameTimeMax;
        stingerTime = stingerTimeMax;
        win = false;
        currentGameIndex = Random.Range(0, listOfGames.Count);
        currentGame = Instantiate(listOfGames[currentGameIndex], Vector3.zero, Quaternion.identity);
        oneshotPlayer.volume = PlayerPrefs.GetFloat("SFX Volume");
        loopPlayer.volume = PlayerPrefs.GetFloat("Music Volume");
        // displaying the hearts
        for (int i = 0; i < lives; i++)
        {
            //hearts.Add(Instantiate(heart, new Vector3(-5.38f, -4.39f + (.6f * i), 0), Quaternion.identity));
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState) {
            case GameState.MiniGame:
                // if the mini game time is greater than -2, decrease the time
                if (miniGameTime >= 0) {
                    miniGameTime -= Time.deltaTime;
                }
                // // if you win
                // if (win) {
                //     gameState = GameState.VictoryStinger;
                // }
                // // if loss / run out of time
                // else if (miniGameTime < 0 && !win) {
                //     gameState = GameState.DefeatStinger;
                // }
                break;
            case GameState.VictoryStinger:
                // count down stinger time
                if (stingerTime >= 0) {
                    stingerTime -= Time.deltaTime;
                }
                else {
                    gameState = GameState.MiniGame;

                    // start the next mini game
                    Debug.Log("Mini Game Start");
                    
                    // reset timers
                    ResetTimers();

                    // get a new game
                    currentGame = GetRandomGame();
                }
                break;
            case GameState.DefeatStinger:
                if (stingerTime >= 0) {
                    stingerTime -= Time.deltaTime;
                }
                else {
                    gameState = GameState.MiniGame;
                    // start the next mini game
                    Debug.Log("Mini Game Start");
                    // reset timers
                    ResetTimers();

                    // get a new game
                    currentGame = GetRandomGame();
                }
                break;
            case GameState.GameOver:
                Debug.Log("Game Over");
                if (stingerTime >= 0) {
                    stingerTime -= Time.deltaTime;
                }
                else {
                    oneshotPlayer.PlayOneShot((AudioClip)Resources.Load("Music/Global/Game Over Loop"));
                    // reset timers
                    ResetTimers();
                    // reset max timers
                    ResetGame();
                }
                break;
            default: 
                throw new System.Exception("Invalid game state");
        }
    }

    public void PlaySound(string path)
    {
        oneshotPlayer.PlayOneShot((AudioClip)Resources.Load("Music/" + path));
    }
    public void PlayLoop(string path)
    {
        loopPlayer.Stop();
        loopPlayer.PlayOneShot((AudioClip)Resources.Load("Music/" + path));
    }
    public void StopLoop()
    {
        loopPlayer.Stop();
    }
    public void StopSounds()
    {
        oneshotPlayer.Stop();
    }
    /// <summary>
    /// End the mini game
    /// </summary>
    /// <param name="didWin">If the player won the mini game</param>
    /// <param name="endGameImmediately">if true, end the game immediately. if false, end the game when timer is up</param>
    public void EndMiniGame(bool didWin, bool endGameImmediately) {
        // end the game immediately when specified
        if (endGameImmediately) {
            miniGameTime = -5.0f;
        }
        // set the win state
        win = didWin;
        if (win) {
            gameState = GameState.VictoryStinger;
            
            // decrease max time (not for the stingers)
            if (roundNumber % 5 == 0 && roundNumber != 0) 
                miniGameTimeMax *= timeDecreaseMultiplier;

            // debug
            StopSounds();
            StopLoop();
            Debug.Log("Victory Stinger");
            oneshotPlayer.PlayOneShot((AudioClip)Resources.Load("Music/Global/Victory Stinger"));
            // increase the round number
            roundNumber++;
            // add to the score
            score += 1;
            // scoreText.text = score.ToString();
            // reset win state
            win = false;
        }
        else {
            gameState = GameState.DefeatStinger;

            Debug.Log("Defeat Stinger");
            

            // add round counter
            roundNumber++;

            // remove a life
            lives--;

            //delete a heart
            // Destroy(hearts[lives]);
            // if you run out of lives, game over
            if (lives == 0) {
                gameState = GameState.GameOver;
                oneshotPlayer.PlayOneShot((AudioClip)Resources.Load("Music/Global/Game Over Stinger"));
            }
            else
            {
                oneshotPlayer.PlayOneShot((AudioClip)Resources.Load("Music/Global/Life Lost"));
            }
        }

        // destroy the current mini game
        Destroy(currentGame);
        currentGame = null;
        // reset timers
        ResetTimers();
    }
    public void ResetTimers() {
        miniGameTime = miniGameTimeMax;
        stingerTime = stingerTimeMax;
    }
    
    /// <summary>
    /// reset all max timers to default values
    /// </summary>
    public void ResetGame() {
        miniGameTimeMax = 10.0f;
        stingerTimeMax = 2.0f;
        roundNumber = 0;
        score = 0;
        lives = 3;
    }

    /// <summary>
    /// Get a random game from the list of games. If current game exists, then do nothing
    /// </summary>
    public GameObject GetRandomGame() {
        if (currentGame == null) {
            do {
                currentGameIndex = Random.Range(0, listOfGames.Count);
            }
            while (currentGameIndex == previousGameIndex || currentGameIndex == previousGameIndex2);

            previousGameIndex2 = previousGameIndex;
            previousGameIndex = currentGameIndex;

            return Instantiate(listOfGames[currentGameIndex]);
        } 
        else {
            throw new System.Exception("Current game already exists!");
        }
    }
}
