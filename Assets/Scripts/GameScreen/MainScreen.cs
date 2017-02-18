using UnityEngine;
using System.Collections;
using StateManager;
using UnityEngine.UI;

public class MainScreen : MonoBehaviour 
{
	public Text BestScoreText;

	void Start () 
	{
		GameModel.Instance.LevelCallback += HandleLevelCallback;
	}

	private void HandleLevelCallback (int level)
	{
		float oldBestScore;
		float.TryParse(BestScoreText.text, out oldBestScore);
		var bestScore = GameModel.Instance.BestScore;

		Debug.Log("HandleLevelCallback:" + bestScore + " level:" + level);

		LeanTween.value( gameObject, oldBestScore, bestScore, 0.5f).setOnUpdate( (float val)=>{ 
			BestScoreText.text = "" + Mathf.Round(val);
		}).setEase(LeanTweenType.easeOutQuad);
	}


	void OnEnable()
	{
		BestScoreText.text = "" + GameModel.Instance.BestScore;
	}


}
