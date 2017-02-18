using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Soomla.Store;

public class Settings : MonoBehaviour 
{
	public Button hideButton;
	public Button facebookButton;
	public Button twitterButton;
	public Button infoButton;
	public Button emailButton;
	public Button restoreButton;

	public GameObject infoPanel;

	public RestoreInfo restoreInfo;

	private bool isRestoring;

	void Start () 
	{
		StoreEvents.OnRestoreTransactionsStarted += OnRestoreTransactionsStarted;
		StoreEvents.OnRestoreTransactionsFinished += OnRestoreTransactionsFinished;

		hideButton.onClick.AddListener(()=>{
			Hide();
		});

		facebookButton.onClick.AddListener(()=>{
			Application.OpenURL("https://www.facebook.com/renkgame");
		});

		twitterButton.onClick.AddListener(()=>{
			Application.OpenURL("https://twitter.com/renkgame");
		});

		infoButton.onClick.AddListener(()=>{
			infoPanel.SetActive(true);
		});

		emailButton.onClick.AddListener(()=>{
			SendEmail();
		});

		restoreButton.onClick.AddListener(()=>{
			isRestoring = true;
			SoomlaStore.RefreshInventory();

		});


	}

	private void SendEmail ()
	{
		string email = "renkgame@gmail.com";
		string subject = MyEscapeURL("Renk Game");
		string body = MyEscapeURL("Hello,\r\nThis game is awesome!");
		Application.OpenURL("mailto:" + email + "?subject=" + subject + "&body=" + body);
	}
	private string MyEscapeURL (string url)
	{
		return WWW.EscapeURL(url).Replace("+","%20");
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

	public void OnRestoreTransactionsStarted() 
	{
		Debug.Log ("Settings" +" onRestoreTransactionsStarted");

		if(isRestoring)
		{
			restoreInfo.Show("Restoring store!");
		}
	}

	public void OnRestoreTransactionsFinished(bool success) 
	{
		Debug.Log ("Settings" +" onRestoreTransactionsFinished");
		if(isRestoring)
		{
			isRestoring = false;

			if(success)
			{
				restoreInfo.Show("Store is restored!");
			}else
			{
				restoreInfo.Show("Store is not restored!");
			}

		}
	}
}
