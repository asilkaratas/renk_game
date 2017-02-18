using System;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;


public class BannerElement : MonoBehaviour, AdsElement 
{
	private BannerView bannerView = null;

	public void Request()
	{
		if(bannerView != null) return;

		#if UNITY_EDITOR
		string adUnitId = "unused";
		#elif UNITY_ANDROID
		string adUnitId = "ca-app-pub-4624212260512946/8767207317";
		#elif UNITY_IPHONE
		string adUnitId = "ca-app-pub-4624212260512946/4197406912";
		#else
		string adUnitId = "unexpected_platform";
		#endif

		
		// Create a 320x50 banner at the top of the screen.
		bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Top);
		// Register for ad events.
		bannerView.AdLoaded += HandleAdLoaded;
		bannerView.AdFailedToLoad += HandleAdFailedToLoad;
		bannerView.AdOpened += HandleAdOpened;
		bannerView.AdClosing += HandleAdClosing;
		bannerView.AdClosed += HandleAdClosed;
		bannerView.AdLeftApplication += HandleAdLeftApplication;
		// Load a banner ad.
		bannerView.LoadAd(createAdRequest());

		//bannerView.Hide();
	}
	
	public void Show()
	{
		bannerView.Show();
	}

	public void Hide()
	{
		bannerView.Hide();
	}

	public void Destroy()
	{
		bannerView.AdLoaded -= HandleAdLoaded;
		bannerView.AdFailedToLoad -= HandleAdFailedToLoad;
		bannerView.AdOpened -= HandleAdOpened;
		bannerView.AdClosing -= HandleAdClosing;
		bannerView.AdClosed -= HandleAdClosed;
		bannerView.AdLeftApplication -= HandleAdLeftApplication;

		bannerView.Destroy();
		bannerView = null;
	}

	public void Refresh()
	{
		if(bannerView != null)
		{
			Destroy();
		}

		Request();
	}


	private AdRequest createAdRequest()
	{
		return new AdRequest.Builder()
			.AddTestDevice(AdRequest.TestDeviceSimulator)
				.AddTestDevice("4790090c95ad90c4")
				.AddKeyword("game")
				.TagForChildDirectedTreatment(false)
				.AddExtra("color_bg", "9B30FF")
				.Build();
		
	}

	public void HandleAdLoaded(object sender, EventArgs args)
	{
		print("HandleAdLoaded event received.");
	}
	
	public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		print("HandleFailedToReceiveAd event received with message: " + args.Message);
	}
	
	public void HandleAdOpened(object sender, EventArgs args)
	{
		print("HandleAdOpened event received");
	}
	
	void HandleAdClosing(object sender, EventArgs args)
	{
		print("HandleAdClosing event received");
	}
	
	public void HandleAdClosed(object sender, EventArgs args)
	{
		print("HandleAdClosed event received");
	}
	
	public void HandleAdLeftApplication(object sender, EventArgs args)
	{
		print("HandleAdLeftApplication event received");
	}

}
