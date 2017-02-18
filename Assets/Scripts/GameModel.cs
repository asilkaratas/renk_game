using UnityEngine;
using System.Collections;
using System;

public class GameModel 
{
	public delegate void OnTotalScore(float totalScore, float oldTotalScore);
	public event OnTotalScore TotalScoreCallback;
	
	public delegate void OnNewBestScore();
	public event OnNewBestScore NewBestScoreCallback;

	public delegate void OnAddScore(float score);
	public event OnAddScore AddScoreCallback;

	public delegate void OnBestScore(float bestScore, float oldBestScore);
	public event OnTotalScore BestScoreCallback;

	public delegate void OnLevel(int level);
	public event OnLevel LevelCallback;

	private string[] bestScoreKeys = {"BestScoreRenk4", "BestScoreRenk5", "BestScoreRenk6"};

	private string lastStoredScoreKey = null;
	private float bestScore = 0.0f;

	private const string levelKey = "level";
	private int level = -1;
	private bool hasNewBestScore;

	private DateTime timeStamp; 

	private string BestScoreKey
	{
		get
		{
			return bestScoreKeys[Level];
		}
	}

	private float totalScore;
	public float TotalScore
	{
		get
		{
			return totalScore;
		}

		set
		{
			var oldTotalScore = totalScore;
			totalScore = value;

			if(TotalScoreCallback != null)
			{
				TotalScoreCallback(totalScore, oldTotalScore);
			}

			if(totalScore > bestScore)
			{
				BestScore = totalScore;
			}
		}
	}

	public void AddScore(float score)
	{
		TotalScore += score;

		if (AddScoreCallback != null) 
		{
			AddScoreCallback(score);
		}
	}

	public float BestScore
	{
		get
		{
			//Debug.Log("BestScoreKey:" + BestScoreKey + " lastStoredScoreKey:" + lastStoredScoreKey);
			if(!BestScoreKey.Equals(lastStoredScoreKey))
			{
				lastStoredScoreKey = BestScoreKey;
				bestScore = PlayerPrefs.GetFloat(BestScoreKey);
			}
			return bestScore;
		}

		set
		{
			var oldBestScore = bestScore;
			bestScore = value;
			PlayerPrefs.SetFloat(BestScoreKey, bestScore);

			lastStoredScoreKey = BestScoreKey;

			if(BestScoreCallback != null)
			{
				BestScoreCallback(bestScore, oldBestScore);
			}

			if(!hasNewBestScore)
			{
				hasNewBestScore = true;
				if(oldBestScore != 0)
				{
					if(NewBestScoreCallback != null)
					{
						NewBestScoreCallback();
					}
				}
			}
		}
	}

	public int Level
	{
		get
		{
			if(level == -1)
			{
				level = PlayerPrefs.HasKey(levelKey) ? PlayerPrefs.GetInt(levelKey) : 0;
			}
			return level;
		}
		
		set
		{
			if(level == value) return;
			
			level = value;
			PlayerPrefs.SetInt(levelKey, level);
			
			if(LevelCallback != null)
			{
				LevelCallback(level);
			}
		}
	}

	private static GameModel instance;
	public static GameModel Instance
	{
		get
		{
			if(instance == null)
			{
				instance = new GameModel();
			}
			return instance;
		}
	}

	public void Reset()
	{
		TotalScore = 0;
		hasNewBestScore = false;
		timeStamp = DateTime.Now;
	}

	private GameModel()
	{
		bestScore = PlayerPrefs.GetFloat ("BestScore");

		Reset();
	}

	public double GetTotalTime()
	{
		return (DateTime.Now - timeStamp).TotalSeconds;
	}

}
