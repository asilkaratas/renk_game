using System;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;


public class GoogleMobileAdsHandler : IInAppPurchaseHandler
{
	private readonly string[] validSkus = { "android.test.purchased" };
	
	//Will only be sent on a success.
	public void OnInAppPurchaseFinished(IInAppPurchaseResult result)
	{
		result.FinishPurchase();
		//AdsManager.OutputMessage = "Purchase Succeeded! Credit user here.";
	}
	
	//Check SKU against valid SKUs.
	public bool IsValidPurchase(string sku)
	{
		foreach(string validSku in validSkus) {
			if (sku == validSku) {
				return true;
			}
		}
		return false;
	}
	
	//Return the app's public key.
	public string AndroidPublicKey
	{
		//In a real app, return public key instead of null.
		get { return null; }
	}
}

public class InterstitialElement : MonoBehaviour 
{
	private InterstitialAd interstitial = null;
	private bool hasFirstRequest = false;

	public void FirstRequest()
	{
		if(!hasFirstRequest)
		{
			hasFirstRequest = true;
			Request();
		}
	}

	public void Request()
	{
		if(interstitial != null) return;

		#if UNITY_EDITOR
		string adUnitId = "unused";
		#elif UNITY_ANDROID
		string adUnitId = "ca-app-pub-4624212260512946/1243940516";
		#elif UNITY_IPHONE
		string adUnitId = "ca-app-pub-4624212260512946/5674140110";
		#else
		string adUnitId = "unexpected_platform";
		#endif


		// Create an interstitial.
		interstitial = new InterstitialAd(adUnitId);
		// Register for ad events.
		interstitial.AdLoaded += HandleInterstitialLoaded;
		interstitial.AdFailedToLoad += HandleInterstitialFailedToLoad;
		interstitial.AdOpened += HandleInterstitialOpened;
		interstitial.AdClosing += HandleInterstitialClosing;
		interstitial.AdClosed += HandleInterstitialClosed;
		interstitial.AdLeftApplication += HandleInterstitialLeftApplication;
		GoogleMobileAdsHandler handler = new GoogleMobileAdsHandler();
		interstitial.SetInAppPurchaseHandler(handler);
		// Load an interstitial ad.
		interstitial.LoadAd(createAdRequest());
	}
	
	public void Show()
	{
		if (interstitial.IsLoaded())
		{
			interstitial.Show();
		}
		else
		{
			print("Interstitial is not ready yet.");
		}
	}
	
	public void Hide()
	{

	}
	
	public void Destroy()
	{
		interstitial.AdLoaded -= HandleInterstitialLoaded;
		interstitial.AdFailedToLoad -= HandleInterstitialFailedToLoad;
		interstitial.AdOpened -= HandleInterstitialOpened;
		interstitial.AdClosing -= HandleInterstitialClosing;
		interstitial.AdClosed -= HandleInterstitialClosed;
		interstitial.AdLeftApplication -= HandleInterstitialLeftApplication;

		//interstitial.SetInAppPurchaseHandler(null);

		interstitial.Destroy();
		interstitial = null;
	}

	public void Refresh()
	{
		Destroy();
		Request ();
	}
	
	
	private AdRequest createAdRequest()
	{
		/*
		return new AdRequest.Builder()
			.AddTestDevice(AdRequest.TestDeviceSimulator)
				.AddTestDevice("0123456789ABCDEF0123456789ABCDEF")
				.AddKeyword("game")
				.SetGender(Gender.Male)
				.SetBirthday(new DateTime(1985, 1, 1))
				.TagForChildDirectedTreatment(false)
				.AddExtra("color_bg", "9B30FF")
				.Build();

*/

		return new AdRequest.Builder()
			.AddTestDevice(AdRequest.TestDeviceSimulator)
				.AddTestDevice("4790090c95ad90c4")
				.AddKeyword("game")
				.TagForChildDirectedTreatment(false)
				.AddExtra("color_bg", "9B30FF")
				.Build();
	}

	public void HandleInterstitialLoaded(object sender, EventArgs args)
	{
		print("HandleInterstitialLoaded event received.");
	}
	
	public void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		print("HandleInterstitialFailedToLoad event received with message: " + args.Message);
	}
	
	public void HandleInterstitialOpened(object sender, EventArgs args)
	{
		print("HandleInterstitialOpened event received");
	}
	
	void HandleInterstitialClosing(object sender, EventArgs args)
	{
		print("HandleInterstitialClosing event received");
	}
	
	public void HandleInterstitialClosed(object sender, EventArgs args)
	{
		print("HandleInterstitialClosed event received");
		
		Refresh();
	}
	
	public void HandleInterstitialLeftApplication(object sender, EventArgs args)
	{
		print("HandleInterstitialLeftApplication event received");
	}
}
