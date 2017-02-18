using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LeaderboardManager : MonoBehaviour 
{
	public Button BestScoresButton;
	private IRenkLeaderboard leaderboard;

	void Start () 
	{
		#if UNITY_ANDROID
		leaderboard = new AndroidLeaderboard();
		#elif UNITY_IPHONE
		leaderboard = new IOSLeaderboard();
		#endif


		leaderboard.Activate();

		BestScoresButton.onClick.AddListener(()=>
		{
			leaderboard.PostScoreAndShowLeaderboard();
		});

	}


}
