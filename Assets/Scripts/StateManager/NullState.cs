using UnityEngine;
using System.Collections;

namespace StateManager{
	
	public class NullState : IGameState {

		private GameStateManager stateManager;

		public NullState(GameStateManager stateManager){
			this.stateManager = stateManager;
		}
		
		public void GoToMainMenu (){
			stateManager.MainScreen.SetActive (true);
			stateManager.State = stateManager.MainMenuState;
		}
		
		public void GoToInGame (){

		}
		
		public void GoToPause (){

		}
		
		public void GoToGameEnd (){

		}

		public void OnEnter()
		{
			
		}
		
		public void OnExit()
		{
			
		}
	}
}
