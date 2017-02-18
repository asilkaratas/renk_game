using UnityEngine;
using System.Collections;

namespace StateManager
{
	public class GameStateManager : MonoBehaviour  
	{

		public GameObject Board;
		public GameObject MainScreen;
		public GameObject GameScreen;
		public GameObject PauseScreen;
		public GameObject GameEndScreen;
		public GameObject AdsManager;
		public RenkStore RenkStore;
		public ScoreScrollView scoreScrollView;
		public Settings settings;
		public GAManager gaManager;


		public IGameState MainMenuState { get; set;}
		public IGameState InGameState { get; set; }
		public IGameState PauseState { get; set; }
		public IGameState GameEndState { get; set; }

		private IGameState state;
		public IGameState State { 
			set{
				state.OnExit();
				state = value;
				state.OnEnter();
			}
		}

		public GameStateManager()
		{

		}

		public void GoToMainMenu (){
			Debug.Log ("Here");
			state.GoToMainMenu ();
		}
		
		public void GoToInGame (){
			state.GoToInGame ();
		}
		
		public void GoToPause (){
			state.GoToPause ();
		}
		
		public void GoToGameEnd (){
			state.GoToGameEnd ();
		}

		void Start () 
		{
			MainMenuState = new MainMenuState (this);
			InGameState = new InGameState (this);
			PauseState = new PauseState (this);
			GameEndState = new GameEndState (this);
			
			state = new NullState (this);

			GoToMainMenu ();
			//MainScreen.SetActive (true);
		}

	}
}
