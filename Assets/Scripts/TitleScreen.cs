using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class TitleScreen : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Begin() {

        SceneManager.LoadScene("Game");
    }
    public void Leaderboard() {

        SceneManager.LoadScene("LeaderBoard");
    }
    public void Quit()
    {
        Application.Quit();
    }

}
