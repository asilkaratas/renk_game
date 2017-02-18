using UnityEngine;
using System.Collections.Generic;

public class WallColorFactory :MonoBehaviour 
{
	private ColorFactory colorFactory;

	void Awake()
	{
		colorFactory = GetComponent<ColorFactory>();
	}

	public List<Color> GetWallColors(int wallSize)
	{
		var wallColors = new List<Color> ();
		var distance = 2;
		var totalWallSize = wallSize * 4;

		for (var i = 0; i < totalWallSize; ++i) {
			var color = GetRandomColor(wallColors, distance);
			wallColors.Add(color);
		}

		return wallColors;
	}

	private Color GetRandomColor(List<Color> wallColors, int distance)
	{
		var removedCount = wallColors.Count > distance ? distance : wallColors.Count;
		var removedColors = new List<Color> ();

		for (var i = 0; i < removedCount; ++i) {
			var removedIndex = wallColors.Count - 1 - i;
			var removedColor = wallColors[removedIndex];

			removedColors.Add(removedColor);
		}

		var color = colorFactory.GetRandomColorWithoutColors (removedColors);
		return color;
	}
}
