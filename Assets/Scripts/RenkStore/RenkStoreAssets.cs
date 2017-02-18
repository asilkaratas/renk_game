using UnityEngine;
using System.Collections;
using Soomla.Store;

public class RenkStoreAssets : IStoreAssets 
{
	public int GetVersion() 
	{
		return 1;
	}

	public VirtualCurrency[] GetCurrencies() 
	{
		return new VirtualCurrency[]{RENK_CURRENCY};
	}

	public VirtualGood[] GetGoods()
	{
		return new VirtualGood[] {RENK5_LTVG, RENK6_LTVG, NO_ADS_LTVG};
	}

	public VirtualCurrencyPack[] GetCurrencyPacks() 
	{
		return new VirtualCurrencyPack[] {TEN_RENK_PACK};
	}

	public VirtualCategory[] GetCategories()
	{
		return new VirtualCategory[]{};
	}

	public const string RENK_CURRENCY_ITEM_ID      = "currency_renk";
	public const string TEN_RENK_PACK_PRODUCT_ID      = "10_renk_pack";
	public const string RENK5_LIFETIME_PRODUCT_ID = "renk5";
	public const string RENK6_LIFETIME_PRODUCT_ID = "renk6";

	#if UNITY_ANDROID
	public const string NO_ADS_LIFETIME_PRODUCT_ID = "no_ads";
	#elif UNITY_IPHONE
	public const string NO_ADS_LIFETIME_PRODUCT_ID = "no_ads2";
	#endif


	public static VirtualCurrency RENK_CURRENCY = new VirtualCurrency(
		"Renk",										// name
		"",												// description
		RENK_CURRENCY_ITEM_ID							// item id
		);

	public static VirtualCurrencyPack TEN_RENK_PACK = new VirtualCurrencyPack(
		"10 Renk",                                   // name
		"Test refund of an item",                       // description
		"renk_10",                                   // item id
		10,												// number of currencies in the pack
		RENK_CURRENCY_ITEM_ID,                        // the currency associated with this pack
		new PurchaseWithMarket(TEN_RENK_PACK_PRODUCT_ID, 0.99)
		);

	public static VirtualGood RENK5_LTVG = new LifetimeVG(
		"5 Renk", 														// name
		"5 Renk Game!",				 									// description
		"renk5",														// item id
		new PurchaseWithMarket(RENK5_LIFETIME_PRODUCT_ID, 0.99));	// the way this virtual good is purchased

	public static VirtualGood RENK6_LTVG = new LifetimeVG(
		"6 Renk", 														// name
		"6 Renk Game!",				 									// description
		"renk6",														// item id
		new PurchaseWithMarket(RENK6_LIFETIME_PRODUCT_ID, 0.99));	// the way this virtual good is purchased

	public static VirtualGood NO_ADS_LTVG = new LifetimeVG(
		"No Ads", 														// name
		"No More Ads!",				 									// description
		NO_ADS_LIFETIME_PRODUCT_ID,														// item id
		new PurchaseWithMarket(NO_ADS_LIFETIME_PRODUCT_ID, 0.99));	// the way this virtual good is purchased
}
