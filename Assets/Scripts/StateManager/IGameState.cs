using UnityEngine;
using System.Collections;


namespace StateManager{

	public interface IGameState {
		void GoToMainMenu ();
		void GoToInGame ();
		void GoToPause ();
		void GoToGameEnd ();

		void OnEnter();
		void OnExit();
	}
}
