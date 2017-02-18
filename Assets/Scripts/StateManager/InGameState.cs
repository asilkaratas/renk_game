using UnityEngine;
using System.Collections;

namespace StateManager
{
	public class InGameState : IGameState 
	{
		private GameStateManager stateManager;

		public InGameState(GameStateManager stateManager)
		{
			this.stateManager = stateManager;
		}


		
		public void GoToMainMenu ()
		{
			stateManager.GameScreen.SetActive (false);
			stateManager.Board.SetActive (false);
			stateManager.State = stateManager.MainMenuState;


		}
		
		public void GoToInGame ()
		{
			
		}
		
		public void GoToPause ()
		{
			var pauseScreen = stateManager.PauseScreen.GetComponent<PauseScreen> ();
			pauseScreen.Show ();

			stateManager.State = stateManager.PauseState;
		}
		
		public void GoToGameEnd ()
		{
			var gameEndScreen = stateManager.GameEndScreen.GetComponent<GameEndScreen> ();
			gameEndScreen.Show ();

			stateManager.State = stateManager.GameEndState;

			stateManager.gaManager.gameSceenLogger.LogGameEndScore((int)GameModel.Instance.TotalScore);
			stateManager.gaManager.gameSceenLogger.LogTimeGameEnd();
		}

		public void OnEnter()
		{
			if(!stateManager.RenkStore.HasNoAds())
			{
				var bannerElement = stateManager.AdsManager.GetComponent<BannerElement> ();
				bannerElement.Show ();
			}

			stateManager.gaManager.screenLogger.LogGameScreen();

		}
		
		public void OnExit()
		{
			if(!stateManager.RenkStore.HasNoAds())
			{
				var bannerElement = stateManager.AdsManager.GetComponent<BannerElement> ();
				bannerElement.Hide ();
			}
		}
	}
}

