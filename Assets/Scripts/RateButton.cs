using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RateButton : MonoBehaviour 
{
	private Button button;

	void Start () 
	{
		button = GetComponent<Button>();
		button.onClick.AddListener(()=>{
			Rate ();
		});
	}

	private void Rate()
	{
		#if UNITY_ANDROID
		Application.OpenURL("market://details?id=co.lowprofile.Renk");
		#elif UNITY_IPHONE
		Application.OpenURL("itms-apps://itunes.apple.com/app/1035167101");
		#endif
	}

}
