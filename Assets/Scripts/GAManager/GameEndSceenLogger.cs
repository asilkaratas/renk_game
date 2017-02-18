using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Soomla.Store;

public class GameEndSceenLogger : MonoBehaviour 
{
	private const string SCREEN_NAME = "Game End Screen";
	private const string CLICK_ACTION = "Click";

	public Button shareButton;
	public Button homeButton;
	public Button replayButton;
	public Button noAdsButton;

	private GAManager gaManager;
	void Start () 
	{
		gaManager = GetComponent<GAManager>();
	
		shareButton.onClick.AddListener(()=>{
			var value = (int)GameModel.Instance.TotalScore;
			gaManager.LogEvent(SCREEN_NAME, CLICK_ACTION, "Share Button", value);
		});

		homeButton.onClick.AddListener(()=>{
			gaManager.LogEvent(SCREEN_NAME, CLICK_ACTION, "Home Button");
		});

		replayButton.onClick.AddListener(()=>{
			gaManager.LogEvent(SCREEN_NAME, CLICK_ACTION, "Replay Button");
		});

		noAdsButton.onClick.AddListener(()=>{
			var value = StoreInventory.GetItemBalance(RenkStoreAssets.NO_ADS_LTVG.ItemId);
			gaManager.LogEvent(SCREEN_NAME, CLICK_ACTION, "No Ads Button", value);
		});
	}
}
