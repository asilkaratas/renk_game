using UnityEngine;
using System.Collections;

public class CellSprite 
{
	private static Sprite instance;

	public static Sprite getInstance()
	{
		if (instance == null)
		{
			int textureSize = 10;
			int totalSize = textureSize * textureSize;
			
			Color[] pixels = new Color[totalSize];
			for (int i = 0; i < totalSize; ++i) 
			{
				pixels[i] = Color.white;
			}
			
			Texture2D texture2d = new Texture2D (textureSize, textureSize);
			texture2d.SetPixels (pixels);
			texture2d.Apply ();
			
			instance = Sprite.Create (texture2d, new Rect (0, 0, textureSize, textureSize), Vector2.zero);
		}

		return instance;
	}
}
