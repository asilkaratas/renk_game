using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameEndScreen : MonoBehaviour 
{
	public GameObject Bg;
	public GameObject Buttons;

	public GameObject ShareScoreButton;
	public Text BestScoreText;
	public GameObject HomeButton;
	public GameObject ReplayButton;
	public GameObject RemoveAdsButton;

	public float Offset = -100f;
	
	void Start()
	{
		Buttons.transform.localPosition = new Vector3 (0f, -Screen.height / 2.0f + Offset, 0);
		Buttons.GetComponent<CanvasGroup> ().blocksRaycasts = false;
	}

	void OnEnable()
	{
		BestScoreText.text = "" + GameModel.Instance.TotalScore;
	}
	
	public void Show()
	{
		gameObject.SetActive (true);
		
		Buttons.transform.localPosition = new Vector3 (0f, -Screen.height / 2.0f + Offset, 0f);
		
		LeanTween.alpha (Bg, 1.0f, 0.3f).setEase (LeanTweenType.easeOutQuad);
		LeanTween.moveLocal (Buttons, Vector3.zero, 0.3f)
			.setEase (LeanTweenType.easeOutBack)
				.setOnComplete (() => {
					Buttons.GetComponent<CanvasGroup> ().blocksRaycasts = true;
				});
	}
	
	public void Hide()
	{
		Buttons.GetComponent<CanvasGroup> ().blocksRaycasts = false;
		
		LeanTween.alpha (Bg, 1.0f, 0f).setEase (LeanTweenType.easeOutQuad);
		LeanTween.moveLocal (Buttons, new Vector3 (0f, -Screen.height / 2.0f + Offset, 0f), 0.3f)
			.setEase (LeanTweenType.easeInBack)
				.setOnComplete(()=>{
					gameObject.SetActive (false);
				});
	}
	

}
