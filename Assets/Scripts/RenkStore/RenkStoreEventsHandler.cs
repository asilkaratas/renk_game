using UnityEngine;
using System.Collections;
using Soomla.Store;
using System.Collections.Generic;

public class RenkStoreEventsHandler 
{
	public RenkStoreEventsHandler()
	{
		StoreEvents.OnMarketPurchase += onMarketPurchase;
		StoreEvents.OnMarketRefund += onMarketRefund;
		StoreEvents.OnItemPurchased += onItemPurchased;
		StoreEvents.OnGoodEquipped += onGoodEquipped;
		StoreEvents.OnGoodUnEquipped += onGoodUnequipped;
		StoreEvents.OnGoodUpgrade += onGoodUpgrade;
		StoreEvents.OnBillingSupported += onBillingSupported;
		StoreEvents.OnBillingNotSupported += onBillingNotSupported;
		StoreEvents.OnMarketPurchaseStarted += onMarketPurchaseStarted;
		StoreEvents.OnItemPurchaseStarted += onItemPurchaseStarted;
		StoreEvents.OnCurrencyBalanceChanged += onCurrencyBalanceChanged;
		StoreEvents.OnGoodBalanceChanged += onGoodBalanceChanged;
		StoreEvents.OnMarketPurchaseCancelled += onMarketPurchaseCancelled;
		StoreEvents.OnRestoreTransactionsStarted += onRestoreTransactionsStarted;
		StoreEvents.OnRestoreTransactionsFinished += onRestoreTransactionsFinished;
		StoreEvents.OnSoomlaStoreInitialized += onSoomlaStoreInitialized;
		StoreEvents.OnUnexpectedStoreError += onUnexpectedStoreError;
	}

	public void onUnexpectedStoreError(int errorCode) {
		Debug.Log ("Soomla/RENK ExampleEventHandler" + " error with code: " + errorCode);
	}
	
	/// <summary>
	/// Handles a market purchase event.
	/// </summary>
	/// <param name="pvi">Purchasable virtual item.</param>
	/// <param name="purchaseToken">Purchase token.</param>
	public void onMarketPurchase(PurchasableVirtualItem pvi, string payload, Dictionary<string, string> extra) {
		Debug.Log ("Soomla/RENK ExampleEventHandler" +" onMarketPurchase");
	}
	
	/// <summary>
	/// Handles a market refund event.
	/// </summary>
	/// <param name="pvi">Purchasable virtual item.</param>
	public void onMarketRefund(PurchasableVirtualItem pvi) {
		Debug.Log ("Soomla/RENK ExampleEventHandler" +" onMarketRefund");
	}
	
	/// <summary>
	/// Handles an item purchase event.
	/// </summary>
	/// <param name="pvi">Purchasable virtual item.</param>
	public void onItemPurchased(PurchasableVirtualItem pvi, string payload) {
		Debug.Log ("Soomla/RENK ExampleEventHandler" +" onItemPurchased");
	}
	
	/// <summary>
	/// Handles a good equipped event.
	/// </summary>
	/// <param name="good">Equippable virtual good.</param>
	public void onGoodEquipped(EquippableVG good) {
		Debug.Log ("Soomla/RENK ExampleEventHandler" +" onGoodEquipped");
	}
	
	/// <summary>
	/// Handles a good unequipped event.
	/// </summary>
	/// <param name="good">Equippable virtual good.</param>
	public void onGoodUnequipped(EquippableVG good) {
		Debug.Log ("Soomla/RENK ExampleEventHandler" +" onGoodUnequipped");
	}
	
	/// <summary>
	/// Handles a good upgraded event.
	/// </summary>
	/// <param name="good">Virtual good that is being upgraded.</param>
	/// <param name="currentUpgrade">The current upgrade that the given virtual
	/// good is being upgraded to.</param>
	public void onGoodUpgrade(VirtualGood good, UpgradeVG currentUpgrade) {
		Debug.Log ("Soomla/RENK ExampleEventHandler" +" onGoodUpgrade");
	}
	
	/// <summary>
	/// Handles a billing supported event.
	/// </summary>
	public void onBillingSupported() {
		Debug.Log ("Soomla/RENK ExampleEventHandler" +" onBillingSupported");
	}
	
	/// <summary>
	/// Handles a billing NOT supported event.
	/// </summary>
	public void onBillingNotSupported() {
		Debug.Log ("Soomla/RENK ExampleEventHandler" +" onBillingNotSupported");
	}
	
	/// <summary>
	/// Handles a market purchase started event.
	/// </summary>
	/// <param name="pvi">Purchasable virtual item.</param>
	public void onMarketPurchaseStarted(PurchasableVirtualItem pvi) {
		Debug.Log ("Soomla/RENK ExampleEventHandler" +" onMarketPurchaseStarted");
	}
	
	/// <summary>
	/// Handles an item purchase started event.
	/// </summary>
	/// <param name="pvi">Purchasable virtual item.</param>
	public void onItemPurchaseStarted(PurchasableVirtualItem pvi) {
		Debug.Log ("Soomla/RENK ExampleEventHandler" +" onItemPurchaseStarted");
	}
	
	/// <summary>
	/// Handles an item purchase cancelled event.
	/// </summary>
	/// <param name="pvi">Purchasable virtual item.</param>
	public void onMarketPurchaseCancelled(PurchasableVirtualItem pvi) {
		Debug.Log ("Soomla/RENK ExampleEventHandler" +" onMarketPurchaseCancelled");
	}
	
	/// <summary>
	/// Handles a currency balance changed event.
	/// </summary>
	/// <param name="virtualCurrency">Virtual currency whose balance has changed.</param>
	/// <param name="balance">Balance of the given virtual currency.</param>
	/// <param name="amountAdded">Amount added to the balance.</param>
	public void onCurrencyBalanceChanged(VirtualCurrency virtualCurrency, int balance, int amountAdded) {
		Debug.Log ("Soomla/RENK ExampleEventHandler" +" onCurrencyBalanceChanged");
	}
	
	/// <summary>
	/// Handles a good balance changed event.
	/// </summary>
	/// <param name="good">Virtual good whose balance has changed.</param>
	/// <param name="balance">Balance.</param>
	/// <param name="amountAdded">Amount added.</param>
	public void onGoodBalanceChanged(VirtualGood good, int balance, int amountAdded) {
		Debug.Log ("Soomla/RENK ExampleEventHandler" +" onGoodBalanceChanged");
	}
	
	/// <summary>
	/// Handles a restore Transactions process started event.
	/// </summary>
	public void onRestoreTransactionsStarted() {
		Debug.Log ("Soomla/RENK ExampleEventHandler" +" onRestoreTransactionsStarted");
	}
	
	/// <summary>
	/// Handles a restore transactions process finished event.
	/// </summary>
	/// <param name="success">If set to <c>true</c> success.</param>
	public void onRestoreTransactionsFinished(bool success) {
		Debug.Log ("Soomla/RENK ExampleEventHandler" +" onRestoreTransactionsFinished");
	}
	
	/// <summary>
	/// Handles a store controller initialized event.
	/// </summary>
	public void onSoomlaStoreInitialized() {
		Debug.Log ("Soomla/RENK ExampleEventHandler" +" onSoomlaStoreInitialized");
	}
}
