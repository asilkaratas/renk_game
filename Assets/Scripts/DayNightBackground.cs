using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DayNightBackground : MonoBehaviour {

	void Start () {
		UserPreferences.Instance.IsNightCallback += HandleIsNightCallback;
	}

	private void HandleIsNightCallback (bool isNight){
		GetComponent<Image> ().color = UserPreferences.Instance.GetDayNightColor ();
	}
}
