using UnityEngine;
using System.Collections;

public class ScreenLogger : MonoBehaviour 
{

	private GAManager gaManager;
	void Awake () 
	{
		gaManager = GetComponent<GAManager>();
	
	}

	public void LogMainScreen()
	{
		gaManager.LogScreen("Main Screen");
	}

	public void LogGameScreen()
	{
		gaManager.LogScreen("Game Screen");
	}

	public void LogPauseScreen()
	{
		gaManager.LogScreen("Pause Screen");
	}

	public void LogGameEndScreen()
	{
		gaManager.LogScreen("Game End Screen");
	}
}
