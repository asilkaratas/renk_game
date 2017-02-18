using UnityEngine;
using System.Collections;

namespace StateManager
{
	public class MainMenuState : IGameState
	{
		private GameStateManager stateManager;

		public MainMenuState(GameStateManager stateManager)
		{
			this.stateManager = stateManager;

			//Debug.Log("MainMenuState:OnEnter:" + stateManager);
		}

		
		public void GoToMainMenu ()
		{
			
		}
		
		public void GoToInGame ()
		{
			stateManager.MainScreen.SetActive (false);
			stateManager.GameScreen.SetActive (true);
			stateManager.Board.SetActive (true);

			stateManager.Board.GetComponent<Board> ().Restart ();

			stateManager.scoreScrollView.Hide();
			stateManager.settings.Hide();

			if(!stateManager.RenkStore.HasNoAds())
			{
				var bannerElement = stateManager.AdsManager.GetComponent<BannerElement> ();
				bannerElement.Refresh();

				var interstitialElement = stateManager.AdsManager.GetComponent<InterstitialElement> ();
				interstitialElement.FirstRequest();
			}

			stateManager.State = stateManager.InGameState;

			stateManager.Board.GetComponent<ToneManager>().PlayGameStartSound();
		}
		
		public void GoToPause ()
		{
			
		}
		
		public void GoToGameEnd ()
		{
			
		}

		public void OnEnter()
		{
			/*
			Debug.Log("MainMenuState:OnEnter:");
			Debug.Log("MainMenuState:OnEnter:" + stateManager);
			Debug.Log("MainMenuState:OnEnter:" + stateManager.gaManager);
			Debug.Log("MainMenuState:OnEnter:" + stateManager.gaManager.screenLogger);
			*/
			stateManager.gaManager.screenLogger.LogMainScreen();
		}
		
		public void OnExit()
		{

		}
	}
}

