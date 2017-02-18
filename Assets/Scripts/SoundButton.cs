using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour {

	public Sprite SoundOffSprite;
	public Sprite SoundOnSprite;

	void Awake(){
		SetSound (UserPreferences.Instance.IsMute);
		UserPreferences.Instance.IsMuteCallback += SetSound;
	}
	
	private void SetSound(bool isMute){
		var image = GetComponent<Image> ();
		image.sprite = isMute ? SoundOffSprite : SoundOnSprite;
	}
	
	public void ToggleSound(){
		UserPreferences.Instance.IsMute = !UserPreferences.Instance.IsMute;
	}
}
