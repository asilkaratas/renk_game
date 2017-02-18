using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RestoreInfo : MonoBehaviour 
{
	public Text text;


	public void Show(string title)
	{
		text.text = title;

		gameObject.SetActive(true);

		LeanTween.cancel(gameObject);

		LeanTween.delayedCall(gameObject, 2.0f, ()=>{
			gameObject.SetActive(false);
		});
	}

}
