using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour 
{
	public SoundManager soundManager;

	void Start () 
	{
		var button = GetComponent<Button>();
		button.onClick.AddListener(()=>{
			soundManager.PlayClickSound();
		});
	}

}
