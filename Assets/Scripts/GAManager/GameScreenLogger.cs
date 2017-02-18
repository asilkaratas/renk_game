using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameScreenLogger : MonoBehaviour
{
	private const string SCREEN_NAME = "Game Screen";

	public Button pauseButton;

	private GAManager gaManager;
	void Start () 
	{
		gaManager = GetComponent<GAManager>();
	
		pauseButton.onClick.AddListener(()=>{
			gaManager.LogEvent(SCREEN_NAME, "Click", "Pause Button");
		});
	}

	public void LogSuccessPlacing()
	{
		gaManager.LogEvent(SCREEN_NAME, "Success", "Placing");
	}

	public void LogFailPlacing()
	{
		gaManager.LogEvent(SCREEN_NAME, "Fail", "Placing");
	}

	public void LogSolvedCount(int count)
	{
		gaManager.LogEvent(SCREEN_NAME, "Count", "Solved", count);
	}

	public void LogGameEndScore(int score)
	{
		gaManager.LogEvent(SCREEN_NAME, "Score", "Game End", score);
	}

	public void LogTimeGameRestart()
	{
		var time = GameModel.Instance.GetTotalTime();
		gaManager.LogTime(SCREEN_NAME, time, "Game Restart", "Game Restart");
	}

	public void LogTimeGameEnd()
	{
		var time = GameModel.Instance.GetTotalTime();
		gaManager.LogTime(SCREEN_NAME, time, "Game End", "Game End");
	}



}
