using UnityEngine;
using System.Collections;

namespace StateManager
{

	public class GameEndState : IGameState 
	{
		
		private GameStateManager stateManager;

		public GameEndState(GameStateManager stateManager)
		{
			this.stateManager = stateManager;
		}
		
		public void GoToMainMenu ()
		{
			var gameEndScreen = stateManager.GameEndScreen.GetComponent<GameEndScreen> ();
			gameEndScreen.Hide ();
			
			stateManager.GameScreen.SetActive (false);
			stateManager.Board.SetActive (false);
			stateManager.Board.GetComponent<Board> ().Reset ();
			
			stateManager.GameEndScreen.SetActive (false);
			stateManager.MainScreen.SetActive (true);
			
			stateManager.State = stateManager.MainMenuState;
		}
		
		public void GoToInGame ()
		{
			var gameEndScreen = stateManager.GameEndScreen.GetComponent<GameEndScreen> ();
			gameEndScreen.Hide ();

			stateManager.Board.GetComponent<Board> ().Restart ();

			if(!stateManager.RenkStore.HasNoAds())
			{
				var bannerElement = stateManager.AdsManager.GetComponent<BannerElement> ();
				bannerElement.Refresh();
			}

			stateManager.State = stateManager.InGameState;

			stateManager.Board.GetComponent<ToneManager>().PlayGameStartSound();
		}

		public void GoToPause (){

		}

		public void GoToGameEnd (){

		}

		public void OnEnter()
		{
			if(!stateManager.RenkStore.HasNoAds())
			{
				var interstitialElement = stateManager.AdsManager.GetComponent<InterstitialElement> ();
				interstitialElement.Show();
			}

			stateManager.Board.GetComponent<ToneManager>().PlayGameEndSound();

			stateManager.gaManager.screenLogger.LogGameEndScreen();
		}

		public void OnExit()
		{

		}
	}

}


