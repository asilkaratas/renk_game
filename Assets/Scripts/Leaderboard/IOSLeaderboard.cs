using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.GameCenter;

public class IOSLeaderboard : IRenkLeaderboard
{
	private bool waitingForAuth;
	private const string Renk4_Leaderboard_Id = "grp.renk4";
	private const string Renk5_Leaderboard_Id = "grp.renk5";
	private const string Renk6_Leaderboard_Id = "grp.renk6";
	
	private string[] leaderboardIds = {Renk4_Leaderboard_Id, Renk5_Leaderboard_Id, Renk6_Leaderboard_Id};


	public void Activate()
	{
		//GooglePlayGames.PlayGamesPlatform.Activate();
		
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
				GameCenterPlatform.ShowLeaderboardUI(leaderboardId, TimeScope.AllTime);
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
