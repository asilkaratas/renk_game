using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseScreenLogger : MonoBehaviour 
{
	private const string SCREEN_NAME = "Pause Screen";
	private const string CLICK_ACTION = "Click";

	public Button homeButton;
	public Button playButton;
	public Button muteButton;
	public Button dayNightButton;

	private GAManager gaManager;
	void Start () 
	{
		gaManager = GetComponent<GAManager>();

		homeButton.onClick.AddListener(()=>{
			gaManager.LogEvent(SCREEN_NAME, CLICK_ACTION, "Play Button");
		});

		playButton.onClick.AddListener(()=>{
			var value = (int)GameModel.Instance.TotalScore;
			gaManager.LogEvent(SCREEN_NAME, CLICK_ACTION, "Home Button", value);
		});

		muteButton.onClick.AddListener(()=>{
			var value = UserPreferences.Instance.IsMute ? 0 : 1;
			gaManager.LogEvent(SCREEN_NAME, CLICK_ACTION, "Mute Button", value);
		});

		dayNightButton.onClick.AddListener(()=>{
			var value = UserPreferences.Instance.IsNight ? 0 : 1;
			gaManager.LogEvent(SCREEN_NAME, CLICK_ACTION, "DayNight Button", value);
		});
	
	}

}
