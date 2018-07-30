using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

	public static ScoreKeeper Instance;

	private void Awake()
	{
		if(Instance == null)
		{
			Instance = this;
		}
		else if(Instance != this)
		{
			Destroy(gameObject);
		}
	}

	public int MatchScore = 5;

	public int Score1 = 0;
	public int Score2 = 0;

	public TextMesh scoreText1 = null;
	public TextMesh scoreText2 = null;

	public bool ScoreME(int player, int score)
	{
		switch(player)
		{
			case 1:
				Score1 += score;
				if(Score1 >= MatchScore)
				{
					return true;
				}
				else
				{
					return false;
				}
			case 2:
				Score2 += score;
				if (Score2 >= MatchScore)
				{
					return true;
				}
				else
				{
					return false;
				}
			default:
				return false;

		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		scoreText1.text = Score1.ToString("00");
		scoreText2.text = Score2.ToString("00");
	}
}
