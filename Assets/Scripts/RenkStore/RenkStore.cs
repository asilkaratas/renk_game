using UnityEngine;
using System.Collections;
using Soomla.Store;
using System.Collections.Generic;
using UnityEngine.UI;

public class RenkStore : MonoBehaviour 
{
	private RenkStoreEventsHandler eventsHandler;
	public Button NoAdsButton;
	public Button NoAdsButton2;

	public Button Renk5Button;
	public Button Renk6Button;

	public Sprite Renk5Sprite;
	public Sprite Renk6Sprite;

	void Start () 
	{
		eventsHandler = new RenkStoreEventsHandler();

		StoreEvents.OnMarketPurchase += OnMarketPurchase;
		StoreEvents.OnRestoreTransactionsFinished += OnRestoreTransactionsFinished;

		SoomlaStore.Initialize(new RenkStoreAssets());

		NoAdsButton.onClick.AddListener(()=>{
			BuyNoAds();
		});

		NoAdsButton2.onClick.AddListener(()=>{
			BuyNoAds();
		});

		CheckButtons();
	}

	private void CheckButtons()
	{
		if(HasNoAds())
		{
			NoAdsButton.gameObject.SetActive(false);
			NoAdsButton2.gameObject.SetActive(false);
		}

		if(HasRenk5())
		{
			Renk5Button.image.sprite = Renk5Sprite;
		}

		if(HasRenk6())
		{
			Renk6Button.image.sprite = Renk6Sprite;
		}
	}

	private void OnMarketPurchase(PurchasableVirtualItem pvi, string payload, Dictionary<string, string> extra) 
	{
		Debug.Log ("Soomla/RENK RenkStore" +" onMarketPurchase");
		CheckButtons();
	}

	private void OnRestoreTransactionsFinished(bool success) {
		Debug.Log ("Soomla/RENK RenkStore" +" onRestoreTransactionsFinished:" + success);
		CheckButtons();
	}


	public void BuyNoAds()
	{
		StoreInventory.BuyItem(RenkStoreAssets.NO_ADS_LTVG.ItemId);
	}

	public void BuyRenk5()
	{
		StoreInventory.BuyItem(RenkStoreAssets.RENK5_LTVG.ItemId);
	}

	public void BuyRenk6()
	{
		StoreInventory.BuyItem(RenkStoreAssets.RENK6_LTVG.ItemId);
	}

	public bool HasNoAds()
	{
		return StoreInventory.GetItemBalance(RenkStoreAssets.NO_ADS_LTVG.ItemId) == 1;
	}

	public bool HasRenk5()
	{
		return StoreInventory.GetItemBalance(RenkStoreAssets.RENK5_LTVG.ItemId) == 1;
	}
	
	public bool HasRenk6()
	{
		return StoreInventory.GetItemBalance(RenkStoreAssets.RENK6_LTVG.ItemId) == 1;
	}
}
