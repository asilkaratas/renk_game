using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour 
{
	public Text TotalScoreText;
	public Text AddedlScoreText;
	public Text BestScoreText;
	public Image ScoreIcon;

	// Use this for initialization
	void Start () 
	{
		GameModel.Instance.AddScoreCallback += HandleAddScore;
		GameModel.Instance.TotalScoreCallback += HandleTotalScore;
		//GameModel.Instance.BestScoreCallback += HandleBestScore;
		GameModel.Instance.NewBestScoreCallback += HandleNewBestScoreCallback;

		TotalScoreText.text = "0";
		BestScoreText.text = "" + GameModel.Instance.BestScore;

		//AddedlScoreText.CrossFadeAlpha (0, 0, true);
		AddedlScoreText.enabled = false;
		LeanTween.textAlpha (AddedlScoreText.rectTransform, 0f, 0.001f);
		LeanTween.delayedCall(1.0f, ()=>{
			AddedlScoreText.enabled = true;
		});

	}

	void OnEnable()
	{
		TotalScoreText.text = "0";
		BestScoreText.text = "" + GameModel.Instance.BestScore;
	}

	private void HandleNewBestScoreCallback ()
	{
		Debug.Log("HandleNewBestScoreCallback");
	}

	private void HandleBestScore (float bestScore, float oldBestScore)
	{
		LeanTween.value( gameObject, oldBestScore, bestScore, 0.5f).setOnUpdate( (float val)=>{ 
			//Debug.Log("tweened val:"+val);
			//BestScoreText.text = "" + Mathf.Round(val);
		} ).setEase(LeanTweenType.easeOutQuad);
	}

	private void HandleTotalScore (float totalScore, float oldTotalScore)
	{
		LeanTween.value( gameObject, oldTotalScore, totalScore, 0.5f).setOnUpdate( (float val)=>{ 
			//Debug.Log("tweened val:"+val);
			TotalScoreText.text = "" + Mathf.Round(val);
		} ).setEase(LeanTweenType.easeOutQuad);
	}

	private void HandleAddScore(float score)
	{
		AddedlScoreText.text = "+" + Mathf.Round(score);

		LeanTween.textAlpha (AddedlScoreText.rectTransform, 1f, 0.01f);
		LeanTween.textAlpha (AddedlScoreText.rectTransform, 0f, 0.3f).setDelay (1f);
	}

}
