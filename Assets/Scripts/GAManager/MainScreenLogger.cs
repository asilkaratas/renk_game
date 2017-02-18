using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Soomla.Store;

public class MainScreenLogger : MonoBehaviour
{
	private const string SCREEN_NAME = "Main Screen";
	private const string CLICK_ACTION = "Click";

	public Button playButton;
	public Button renk4Button;
	public Button renk5Button;
	public Button renk6Button;
	public Button bestScoreButton;
	public Button rankingButton;
	public Button ratingButton;
	public Button noAdsButton;
	public Button dayNightButton;

	private GAManager gaManager;
	void Start () 
	{
		gaManager = GetComponent<GAManager>();

		playButton.onClick.AddListener(()=>{
			gaManager.LogEvent(SCREEN_NAME, CLICK_ACTION, "Play Button");
		});

		renk4Button.onClick.AddListener(()=>{
			gaManager.LogEvent(SCREEN_NAME, CLICK_ACTION, "Renk 4 Button");
		});

		renk5Button.onClick.AddListener(()=>{
			var value = StoreInventory.GetItemBalance(RenkStoreAssets.RENK5_LTVG.ItemId);
			gaManager.LogEvent(SCREEN_NAME, CLICK_ACTION, "Renk 5 Button", value);
		});

		renk6Button.onClick.AddListener(()=>{
			var value = StoreInventory.GetItemBalance(RenkStoreAssets.RENK6_LTVG.ItemId);
			gaManager.LogEvent(SCREEN_NAME, CLICK_ACTION, "Renk 6 Button", value);
		});

		bestScoreButton.onClick.AddListener(()=>{
			var value = Social.localUser.authenticated ? 1 : 0;
			gaManager.LogEvent(SCREEN_NAME, CLICK_ACTION, "Best Score Button", value);
		});

		rankingButton.onClick.AddListener(()=>{
			var value = FB.IsLoggedIn ? 1 : 0;
			gaManager.LogEvent(SCREEN_NAME, CLICK_ACTION, "Ranking Button", value);
		});

		ratingButton.onClick.AddListener(()=>{
			gaManager.LogEvent(SCREEN_NAME, CLICK_ACTION, "Rating Button");
		});

		noAdsButton.onClick.AddListener(()=>{
			var value = StoreInventory.GetItemBalance(RenkStoreAssets.NO_ADS_LTVG.ItemId);
			gaManager.LogEvent(SCREEN_NAME, CLICK_ACTION, "No Ads Button", value);
		});

		dayNightButton.onClick.AddListener(()=>{
			var value = UserPreferences.Instance.IsNight ? 0 : 1;
			gaManager.LogEvent(SCREEN_NAME, CLICK_ACTION, "DayNight Button", value);
		});
	}

}
