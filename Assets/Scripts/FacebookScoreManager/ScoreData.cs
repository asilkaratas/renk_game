using UnityEngine;
using System.Collections;

public class ScoreData
{
	public delegate void OnTextureChanged();
	public event OnTextureChanged TextureChangedCallback;

	public int score;
	public string userId;
	public string username;

	private Texture2D texture = null;
	public Texture2D Texture
	{
		get
		{
			return texture;
		}

		set
		{
			texture = value;

			if(TextureChangedCallback != null)
			{
				TextureChangedCallback();
			}
		}
	}
}
