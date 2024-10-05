using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {
    MiniGame,
    VictoryStinger,
    DefeatStinger,
    GameOver
}
public class Manager : Singleton<Manager>
{
    [SerializeField] private int score = 0;
    [SerializeField] private int lives = 3;
    
    [SerializeField] private float timeDecreaseMultiplier = 0.9f;
    [SerializeField] private float playbackSpeed;
    [SerializeField] private GameState gameState;
    [SerializeField] private List<GameObject> listOfGames;

    // timers
    [SerializeField] private float miniGameTime;
    [SerializeField] private float miniGameTimeMax = 10.0f;

    // used for game over screen as well
    [SerializeField] private float stingerTime;
    [SerializeField] private float stingerTimeMax = 2.0f;

    // game state info
    [SerializeField] private bool win;
    [SerializeField] private int roundNumber = 0;


    // Start is called before the first frame update
    void Start()
    {
        gameState = GameState.MiniGame;
        miniGameTime = miniGameTimeMax;
        stingerTime = stingerTimeMax;
        win = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState) {
            case GameState.MiniGame:
                // if the mini game time is greater than 0, decrease the time
                if (miniGameTime >= 0) {
                    miniGameTime -= Time.deltaTime;
                }
                // if you win
                if (win) {
                    gameState = GameState.VictoryStinger;
                    // decrease max time (not for the stingers)
                    miniGameTimeMax *= timeDecreaseMultiplier;
                    // debug
                    Debug.Log("Victory Stinger");
                    // increase the round number
                    roundNumber++;
                    // add to the score
                    score += 100;
                    // reset win state
                    win = false;
                    // reset timers
                    ResetTimers();
                }
                // if loss / run out of time
                else if (miniGameTime < 0 && !win) {
                    gameState = GameState.DefeatStinger;
                    // reset timers
                    ResetTimers();

                    Debug.Log("Defeat Stinger");

                    // add round counter
                    roundNumber++;

                    // remove a life
                    lives--;
                    // if you run out of lives, game over
                    if (lives == 0) {
                        gameState = GameState.GameOver;
                    } 
                }
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
                }
                break;
            default: 
                throw new System.Exception("Invalid game state");
                break;
        }
    }

    /// <summary>
    /// End the mini game
    /// </summary>
    /// <param name="didWin">If the player won the mini game</param>
    /// <param name="endGameImmediately">if true, end the game immediately. if false, end the game when timer is up</param>
    public void EndMiniGame(bool didWin, bool endGameImmediately) {
        // end the game immediately when specified
        if (endGameImmediately) {
            miniGameTime = -1.0f;
        }
        // set the win state
        win = didWin;
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
}
