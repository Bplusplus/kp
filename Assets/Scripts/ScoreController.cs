using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class ScoreController : MonoBehaviour {


    public GifPlayerMesh[] boards;
    // public GifData[] gifs;

    public GifData[] easyGifs;

    public GifData[] mediumGifs;

    public GifData[] hardGifs;

    public int numOfGifs = 3;


    int[] playerScores;
    public TextMesh[] scoreText;

    public int targetScore = 10;
    public RoundManager rm;
    public DistortController dc;
    private void Start()

    {

        Timer.TimesUp += TimerDone;

    }


    // Use this for initialization
    void Awake() {

      playerScores = new int[2];

    }

    // Update is called once per frame
    void Update() {

    }

    public void UpdateScore(float xpos){
        if (xpos < 0)
        {
            playerScores[1]++;
            scoreText[1].text = playerScores[1].ToString();
            UpdateMesh(1);
           
            if (playerScores[1] >= targetScore)
            {

                rm.gameOver(1,playerScores[1], "Player Two",0, playerScores[0], "Player One");

            }

        }

        else
        {
            
            playerScores[0]++;
            scoreText[0].text = playerScores[0].ToString();
            UpdateMesh(0);
            if (playerScores[0] >= targetScore)

            {

                rm.gameOver(0,playerScores[0], "Player One", 1,playerScores[1], "Player Two");

            }

    }
        dc.UpdateDistortion(WhoIsLeader(), WhatIsLead());
    }
    void UpdateMesh(int i)
    {

        // boards[i].ChangeGif(gifs[playerScores[i] % 3]);
        int r = Random.Range(0, 3);
        if (playerScores[i] < 4)

        {


            boards[i].ChangeGif(easyGifs[r % 3]);



        }

        else if (playerScores[i] > 6)

        {

            boards[i].ChangeGif(hardGifs[r % 3]);



        }

        else
        {

            boards[i].ChangeGif(mediumGifs[r % 3]);



        }

        // boards[i].ChangeGif(gifs[playerScores[i]%3]);


    }
    public void ResetScore()

{

    playerScores[0] = 0;

    playerScores[1] = 0;

    scoreText[0].text = "0";

    scoreText[1].text = "0";

    UpdateMesh(0);

    UpdateMesh(0);
    dc.resetDistortion();


    }

    public void TimerDone()
{



    int winnerScore = playerScores[0];
    int winningPlayer = 0;
    int losingPlayer = 1;

    if (winnerScore < playerScores[1])

    {
        winningPlayer = 1;
            losingPlayer = 0;
        
        winnerScore = playerScores[1];

        rm.gameOver(winningPlayer,winnerScore, "Player Two", losingPlayer, playerScores[0], "Player One");

    }

    else
    {

        rm.gameOver(winningPlayer,winnerScore, "Player One", losingPlayer, playerScores[1], "Player Two");

    }



}
    public int WhoIsLeader() {
        if (playerScores[0] > playerScores[1])
        {
            return 0;
        }
        else if (playerScores[1] > playerScores[0])
        {
            return 1;
        }
        else return 2;

    }
    public int WhatIsLead() {
        int leader = WhoIsLeader();
        switch (leader) {
            case 0:
                return playerScores[0] - playerScores[1];
            case 1:
                return playerScores[1] - playerScores[0];

            default:
                return 0;
                

        }
    }
 
}
