using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private GameObject heart;
    private Stack<GameObject> hearts = new Stack<GameObject>();

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

    // stinger items
    [SerializeField] private TMP_Text stingerScoreText;
    [SerializeField] private GameObject tvStatic;
    [SerializeField] private GameObject returnButtonPrefab;

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
            hearts.Push(Instantiate(heart, new Vector3(-5.38f, -4.39f + (.6f * i), 0), Quaternion.identity));
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if active, turn off the stinger score text
        if (stingerScoreText.gameObject.activeSelf)
        {
            stingerScoreText.gameObject.SetActive(false);
            tvStatic.SetActive(false);
        }

        // switch statement for game state
        switch (gameState) {
            case GameState.MiniGame:
                // if the mini game time is greater than -2, decrease the time
                if (miniGameTime >= 0) {
                    miniGameTime -= Time.deltaTime;
                }

                // TODO: rewrite this so no error
                ////hearts and score count reappear during games
                //for (int i = 0; i < hearts.Count; i++)
                //{
                //    hearts[i].GetComponent<Renderer>().enabled = true;
                //}

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
                // Show the stinger score text
                if (!stingerScoreText.gameObject.activeSelf)
                {
                    stingerScoreText.gameObject.SetActive(true);
                    tvStatic.SetActive(true);
                }

                SetStingerScore();
                // count down stinger time
                if (stingerTime >= 0) {
                    stingerTime -= Time.deltaTime;

                    // TODO: rewrite this so no error
                    ////hearts and score disappear during stinger
                    //for (int i = 0; i < hearts.Count; i++)
                    //{
                    //    hearts[i].GetComponent<Renderer>().enabled = false;
                    //}
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
                // Show the stinger score text
                if (!stingerScoreText.gameObject.activeSelf)
                {
                    stingerScoreText.gameObject.SetActive(true);
                    tvStatic.SetActive(true);
                }

                SetStingerScore();
                if (stingerTime >= 0) {
                    stingerTime -= Time.deltaTime;

                    // TODO: rewrite this so no error
                    //hearts and score disappear during stinger
                    //for (int i = 0; i <= hearts.Count; i++)
                    //{
                    //    hearts[i].GetComponent<Renderer>().enabled = false;
                    //}
                    //scoreText.gameObject.SetActive(false);
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
                    
                    // reset timers
                    ResetTimers();
                    // reset max timers
                    ResetGame();
                    GameObject button = Instantiate(returnButtonPrefab, GameObject.Find("Canvas").transform);
                    button.SetActive(true);
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
            score += 100;
            levelText.text = roundNumber.ToString();
            // reset win state
            win = false;
        }
        else {
            gameState = GameState.DefeatStinger;

            Debug.Log("Defeat Stinger");
            

            // add round counter
            roundNumber++;

            // remove a life
            // lives--;
            DecreaseLives();

            //TODO: rewrite this so no error
            //delete a heart
            //Destroy(hearts[lives]);
            // if you run out of lives, game over
            if (lives == 0) {
                gameState = GameState.GameOver;
                StopLoop();
                oneshotPlayer.PlayOneShot((AudioClip)Resources.Load("Music/Global/Game Over Stinger"));
                PlayLoop("Global/Game Over Loop");
                if (PlayerPrefs.GetInt("High Score") < score)
                {
                    PlayerPrefs.SetInt("Highscore", score);
                }
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
            while (currentGameIndex == previousGameIndex && currentGameIndex == previousGameIndex2);

            previousGameIndex2 = previousGameIndex;
            previousGameIndex = currentGameIndex;

            return Instantiate(listOfGames[currentGameIndex]);
        } 
        else {
            throw new System.Exception("Current game already exists!");
        }
    }

    public void DecreaseLives()
    {
        lives--;
        Destroy(hearts.Pop());
    }

    public void SetStingerScore()
    {
        stingerScoreText.text = "Score: " + score;
    }
    public void ReturnMenu()
    {
        Debug.Log("Returning to menu");
        SceneManager.LoadScene("Intro Scene");
    }
}
