using UnityEngine;
using System.Collections;

public class CameraDayNight : MonoBehaviour {

	void Awake(){

		SetDayNight (UserPreferences.Instance.IsNight);
		UserPreferences.Instance.IsNightCallback += SetDayNight;

	}

	private void SetDayNight(bool isNight){

		var camera = GetComponent<Camera> ();
		camera.backgroundColor = UserPreferences.Instance.GetDayNightColor();

	}
}
