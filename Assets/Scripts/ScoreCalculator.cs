using UnityEngine;
using System.Collections;
using System;

public class ScoreCalculator : MonoBehaviour 
{
	private int[] scores = new int[100];
	// Use this for initialization
	void Start () {
		InitScore();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void InitScore()
	{
		int score = 1;
		int dif = 0;
		int difIncrement = 1;
		int scoreStored = 0;
		for(int i = 0; i < 100; ++i)
		{
			score += dif;
			dif += difIncrement;

			scoreStored = (int)Math.Floor(score/2.0f) + 1;
			scores[i] =  scoreStored;
			//Debug.Log("InitScore:score:" + scoreStored);
		}
	}

	public int GetScore(int count)
	{
		if(count < 0 && count > 100)
		{
			return 0;
		}

		return scores[count -1];
	}
}
