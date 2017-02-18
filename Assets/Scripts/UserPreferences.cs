using UnityEngine;
using System.Collections;

public class UserPreferences {

	private bool isNight;
	public bool IsNight{
		get{
			return isNight;
		}

		set{
			if(isNight != value){
				isNight = value;
				if(IsNightCallback != null){
					IsNightCallback(isNight);
				}
			}
		}
	}

	public delegate void OnIsNight(bool isNight);
	public event OnIsNight IsNightCallback;

	private bool isMute;
	public bool IsMute{
		get{
			return isMute;
		}
		
		set{
			if(isMute != value){
				isMute = value;
				if(IsMuteCallback != null){
					IsMuteCallback(isMute);
				}
			}
		}
	}
	
	public delegate void OnIsMute(bool isMute);
	public event OnIsMute IsMuteCallback;


	public Color NightColor = new Color(0.14f, 0.14f, 0.14f, 0.5f);
	public Color DayColor = new Color (1f, 1f, 1f, 0.5f);

	public Color GetDayNightColor(){
		return isNight ? NightColor : DayColor;
	}

	private static UserPreferences instance;
	public static UserPreferences Instance{
		get{
			if(instance == null){
				instance = new UserPreferences();
			}
			return instance;
		}
	}

	private UserPreferences(){

	}
}
