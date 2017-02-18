using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DayNightButton : MonoBehaviour 
{
	public Sprite NightSprite;
	public Sprite DaySprite;

	void Awake()
	{
		SetDayNight (UserPreferences.Instance.IsNight);
		UserPreferences.Instance.IsNightCallback += SetDayNight;
	}


	private void SetDayNight(bool isNight)
	{
		var image = GetComponent<Image> ();
		image.sprite = isNight ? NightSprite : DaySprite;
	}

	public void ToggleDayNight()
	{
		UserPreferences.Instance.IsNight = !UserPreferences.Instance.IsNight;
	}
}
