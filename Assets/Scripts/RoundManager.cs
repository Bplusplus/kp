using System;

using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using UnityEngine.UI;




public class RoundManager : MonoBehaviour
{



    int rounds = 0;

    public int MaxNumOfRounds = 3;

    public ScoreController sc;

    public AudioManager am;

    public Timer tm;

    public BallMovement bm;

    //public TextMesh winScreen;
    public GameObject ball;
    bool gameOverPause = false;
    bool isPaused = false;
    public Sprite[] winner;
    public Image winUi;
    public bool toggle = false;
    // Use this for initialization

    void Start()
    {
       // winScreen.text = "";
      //  bm = ball.GetComponent<BallMovement>();


    }



    // Update is called once per frame

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Backspace)) {
           
            StartCoroutine(pauseGame());

            
        }
        if (gameOverPause &&!isPaused) {
            isPaused = true;
            StartCoroutine(pauseGame());

        }
        if (Input.GetKeyDown(KeyCode.Y)) {
            NewGame();
        }
    }



    public void gameOver(int winningPlayer,int winningPlayerScore, string winningPlayerName, int losingPlayer, int losingPlayerScore, string losingPlayerName)
    {
       
           // winScreen.text = "Congrats " + winningPlayerName + "! You won with " + winningPlayer + " points! /n Don't Worry " + losingPlayer + " you'll get them next time!";
        winUi.sprite = winner[winningPlayer];
        gameOverPause = true;
       
       // winScreen.text = "";

    }

    public IEnumerator pauseForNewPlayers() {
        Time.timeScale = 0.0001f;
        yield return StartCoroutine(waitForInput(KeyCode.Space));

        Time.timeScale = 1;
      


    }
    IEnumerator waitForInput(KeyCode key) {

        while (!Input.GetKeyDown(key)) {
            yield return null;
        }
      
    }
    public IEnumerator pauseGame() {

        bm.pause();
        tm.pauseTimer();
        toggle = !toggle;
        winUi.enabled = toggle;
        yield return  StartCoroutine(waitForInput(KeyCode.Space));
        
        toggle = !toggle;
        gameOverPause = false;
        isPaused = false;
        winUi.enabled = toggle;
        bm.pause();
        tm.StartTimer();
        NewGame();

    }
    void NewGame() {
      //  StartCoroutine(pauseGame());
        sc.ResetScore();

        tm.resetTimer();

        bm.Spawn();
    }
}
