using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;

public class AndroidLeaderboard : IRenkLeaderboard 
{
	private bool waitingForAuth;
	private const string Renk4_Leaderboard_Id = "CggIzK6lpF4QAhAB";
	private const string Renk5_Leaderboard_Id = "CggIzK6lpF4QAhAC";
	private const string Renk6_Leaderboard_Id = "CggIzK6lpF4QAhAD";
	
	private string[] leaderboardIds = {Renk4_Leaderboard_Id, Renk5_Leaderboard_Id, Renk6_Leaderboard_Id};

	public void Activate()
	{
		PlayGamesPlatform.Activate();

		GameModel.Instance.BestScoreCallback += HandleBestScoreCallback;
	}

	private void HandleBestScoreCallback (float totalScore, float oldTotalScore)
	{
		PostScoreSilently((int)totalScore);
	}

	private void Authenticate(System.Action<bool> callback)
	{
		if(Social.localUser.authenticated)
		{
			callback(true);
		}
		else if(!waitingForAuth)
		{
			Social.localUser.Authenticate((bool success) => 
			                              {
				waitingForAuth = false;
				callback(success);
			});
		}
	}
	
	private void ShowLeaderboard()
	{
		Authenticate((bool success) => 
		             {
			if(success)
			{
				var leaderboardId = GetLeaderboardId();
				PlayGamesPlatform.Instance.ShowLeaderboardUI(leaderboardId);
			}
		});
	}
	
	
	public void PostScoreAndShowLeaderboard()
	{
		int score = (int)GameModel.Instance.BestScore;
		if(score != 0)
		{
			PostScore(score, (bool success) => {
				ShowLeaderboard();
			});
		}
		else
		{
			ShowLeaderboard();
		}
		
	}
	
	private void PostScore(int score, System.Action<bool> callback)
	{
		Authenticate((bool success) => 
		             {
			if(success)
			{
				var leaderboardId = GetLeaderboardId();
				Social.ReportScore(score, leaderboardId, (bool scoreSuccess) => {
					callback(scoreSuccess);
				});
			}
			else
			{
				callback(false);
			}
		});
	}
	
	private void PostScoreSilently(int score)
	{
		if(Social.localUser.authenticated && score != 0)
		{
			var leaderboardId = GetLeaderboardId();
			Social.ReportScore(score, leaderboardId, (bool scoreSuccess) => {});
		}
	}
	
	
	private string GetLeaderboardId()
	{
		var level = GameModel.Instance.Level;
		var leaderboardId = leaderboardIds[level];
		return leaderboardId;
	}

}
