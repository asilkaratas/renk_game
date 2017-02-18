using UnityEngine;
using System.Collections;

namespace StateManager{
	
	public class PauseState : IGameState {
		
		private GameStateManager stateManager;

		public PauseState(GameStateManager stateManager){
			this.stateManager = stateManager;
		}
		
		public void GoToMainMenu ()
		{
			if(!stateManager.RenkStore.HasNoAds())
			{
				var interstitialElement = stateManager.AdsManager.GetComponent<InterstitialElement> ();
				interstitialElement.Show();
			}

			var pauseScreen = stateManager.PauseScreen.GetComponent<PauseScreen> ();
			pauseScreen.Hide ();

			stateManager.GameScreen.SetActive (false);
			stateManager.Board.SetActive (false);
			stateManager.Board.GetComponent<Board> ().Reset ();

			stateManager.PauseScreen.SetActive (false);
			stateManager.MainScreen.SetActive (true);

			stateManager.State = stateManager.MainMenuState;

			stateManager.gaManager.gameSceenLogger.LogTimeGameRestart();
		}
		
		public void GoToInGame ()
		{
			Debug.Log ("GoToInGame");
			var pauseScreen = stateManager.PauseScreen.GetComponent<PauseScreen> ();
			pauseScreen.Hide ();


			stateManager.State = stateManager.InGameState;
		}
		
		public void GoToPause (){
			
		}
		
		public void GoToGameEnd (){
			
		}

		public void OnEnter()
		{
			stateManager.gaManager.screenLogger.LogPauseScreen();
		}
		
		public void OnExit()
		{

		}
	}
}
