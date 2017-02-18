using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreScrollView : MonoBehaviour 
{
	public GameObject scoreViewPrefab;
	public Button hideButton;

	private ScrollRect scrollRect;

	private bool isCreated = false;

	private ScoreData[] scores;
	public ScoreData[] Scores
	{
		set
		{
			scores = value;
		}
	}

	void Start () 
	{
		scrollRect = GetComponentInChildren<ScrollRect>();

		hideButton.onClick.AddListener(()=>{
			Hide();
		});
	}

	private void CreateScores()
	{
		if(isCreated)
		{
			return;
		}
		isCreated = true;

		Debug.Log ("ScoreScrollView:CreateScores:" + scores.Length);

		var count = scores.Length;
		var space = 20;
		var width = 75;
		var totalSize = count * width + (count + 1) * space;
		var offset = totalSize / 2;
		var pos = Vector3.zero;
		pos.x = -offset + width/2 + space;
		for(int i = 0; i < count; ++i)
		{
			var score = scores[i];

			var scoreViewInstance = Instantiate(scoreViewPrefab, pos, Quaternion.identity) as GameObject;
			scoreViewInstance.transform.SetParent(scrollRect.content, false);

			var scoreView = scoreViewInstance.GetComponent<ScoreView>();
			scoreView.Score = score;
			
			pos.x += width + space;
		}
		
		var sizeDelta = scrollRect.content.sizeDelta;
		sizeDelta.x = totalSize - Screen.width;
		scrollRect.content.sizeDelta = sizeDelta;
	}

	public void Hide()
	{
		LeanTween.moveY(gameObject, -240-90.0f, 0.3f)
			.setEase(LeanTweenType.easeInBack)
				.setOnComplete(()=>{
					gameObject.SetActive(false);
				});
	}
	
	public void Show()
	{
		gameObject.SetActive(true);
		LeanTween.moveY(gameObject,  -240 + 90.0f, 0.3f)
			.setEase(LeanTweenType.easeOutBack);
	}


}
