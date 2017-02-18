using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine.UI;

public class FacebookScoreManager : MonoBehaviour 
{
	private int postScoreAttempt = 0;
	private int maxPostScoreAttempt = 3;

	public Button showButton;
	public Button shareButton;

	private ScoreScrollView scoreScrollView;

	void Start ()
	{
		scoreScrollView = GetComponent<ScoreScrollView>();

		FB.Init(OnInitComplete, OnHideUnity);

		showButton.onClick.AddListener(()=>{
			PostScoreAndShow((int)GameModel.Instance.BestScore);
			//scoreScrollView.Show();
		});

		shareButton.onClick.AddListener(()=>{
			ShareScore();
		});

		GameModel.Instance.BestScoreCallback += HandleBestScoreCallback;
	}
	
	private void HandleBestScoreCallback (float totalScore, float oldTotalScore)
	{
		PostScoreSilently((int)totalScore);
	}

	private void OnInitComplete()
	{
		Debug.Log("FB.Init completed: Is user logged in? " + FB.IsLoggedIn);
	}
	
	private void OnHideUnity(bool isGameShown)
	{
		Debug.Log("Is game showing? " + isGameShown);
	}

	public void PostScore(int score, Facebook.FacebookDelegate callback = null)
	{
		if (!FB.IsLoggedIn)
		{
			FB.Login("public_profile,user_friends,email,publish_actions", (FBResult result)=>{

				postScoreAttempt ++;
				if(FB.IsLoggedIn)
				{
					PostScoreAction(score, callback);
				}
				else if(postScoreAttempt < maxPostScoreAttempt)
				{
					//PostScore(score, callback);
				}
			});
		}
		else
		{
			PostScoreAction(score, callback);
		}
	}

	private void PostScoreAction(int score, Facebook.FacebookDelegate callback = null)
	{
		string query = "/" + FB.UserId + "/scores";
		Dictionary<string, string> formData = new Dictionary<string, string>(){{"score", score.ToString()}};
		
		FB.API(query, Facebook.HttpMethod.POST, (FBResult result)=>{
			Debug.Log("OnPostScore:" + result.Text);
			if(callback != null)
			{
				callback(result);
			}
		}, formData);
	}

	public void PostScoreSilently(int score)
	{
		if(FB.IsLoggedIn)
		{
			PostScoreAction(score);
		}
	}

	public void PostScoreAndShow(int score)
	{
		postScoreAttempt = 0;
		PostScore(score, (FBResult result)=>{
			GetScore();
		});
	}

	private void GetScore()
	{
		Debug.Log("GetScores");
		
		string query = "/" + FB.AppId + "/scores?fields=score,user.limit(10)";
		
		FB.API(query, Facebook.HttpMethod.GET, OnGetScores);
	}

	private void OnGetScores(FBResult result)
	{
		var json = JSON.Parse(result.Text);
		var scoresObj = json["data"];

		Debug.Log("OnGetScores:" + result.Text);
		Debug.Log("scores:" + scoresObj.Count);

		var count = scoresObj.Count;

		var scores = new ScoreData[count];
		for(int i = 0; i < count; ++i)
		{
			var scoreObj = scoresObj[i];

			var score = new ScoreData();
			score.score = scoreObj["score"].AsInt;
			score.userId = scoreObj["user"]["id"];
			score.username = scoreObj["user"]["name"];

			LoadPictureAPI(Util.GetPictureURL(score.userId, 128, 128), (Texture2D texture)=>{
				score.Texture = texture;
			});

			scores[i] = score;
		}

		scoreScrollView.Scores = scores;
		scoreScrollView.Show();
	}



	delegate void LoadPictureCallback (Texture2D texture);
	IEnumerator LoadPictureEnumerator(string url, LoadPictureCallback callback)    
	{
		WWW www = new WWW(url);
		yield return www;
		callback(www.texture);
	}
	void LoadPictureAPI(string url, LoadPictureCallback callback)
	{
		FB.API(url,Facebook.HttpMethod.GET,result =>
		       {
			if (result.Error != null)
			{
				Debug.LogError("LoadPictureAPI:Error:" + result.Error);
				return;
			}
			
			var imageUrl = Util.DeserializePictureURLString(result.Text);
			
			StartCoroutine(LoadPictureEnumerator(imageUrl,callback));
		});
	}
	void LoadPictureURL (string url, LoadPictureCallback callback)
	{
		StartCoroutine(LoadPictureEnumerator(url,callback));
		
	}

	public void ShareScore()
	{
		if (!FB.IsLoggedIn)
		{
			FB.Login("public_profile,user_friends,email,publish_actions", (FBResult result)=>{
				if(FB.IsLoggedIn)
				{
					ShareScoreAction();
				}
			});
		}
		else
		{
			ShareScoreAction();
		}

	}

	private void ShareScoreAction()
	{
		FB.Feed(
			linkCaption: "I just got " + GameModel.Instance.TotalScore.ToString() + " points! Can you beat it?",
			picture: "http://renk-game.appspot.com/img/logo_256x256.png",
			linkName: "Renk Puzzle Game!",
			link: "http://renk-game.appspot.com");
	}
}
