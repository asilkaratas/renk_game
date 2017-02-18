using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreView : MonoBehaviour
{
	public Text nameText;
	public Text scoreText;
	public Image userImage;

	private ScoreData score = null;
	public ScoreData Score
	{
		set
		{
			if(score != null)
			{
				score.TextureChangedCallback -= HandleTextureChangedCallback;
			}

			score = value;

			Debug.Log ("ScoreView:Score.set:");

			if(score != null)
			{
				score.TextureChangedCallback += HandleTextureChangedCallback;
			}
		}
	}

	private void HandleTextureChangedCallback ()
	{
		Debug.Log ("ScoreView:HandleTextureChangedCallback:score.Texture:" + score.Texture);
		if(userImage != null)
		{
			UpdateUserImage();
		}

	}

	void Start () 
	{
		Debug.Log ("ScoreView:Start:score.Texture:" + score.Texture);
		nameText.text = score.username;
		scoreText.text = score.score.ToString();

		if(score.Texture != null)
		{
			UpdateUserImage();
		}
	}

	private void UpdateUserImage()
	{
		var texture = score.Texture;
		Sprite sprite = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), Vector2.zero);
		
		userImage.sprite = sprite;
	}

}
