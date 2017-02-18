using UnityEngine;
using System.Collections.Generic;

public class ColorFactory : MonoBehaviour
{
	private List<Color> colors;
	private int size;
	public int Size
	{
		get
		{
			return size;
		}

		set
		{
			size = value;
			Reset ();
		}
	}

	private static Color transparent;
	public static Color Transparent{
		get{
			if(transparent == null){
				transparent = Color.white;
				string hexColor = "FFFFFF00";
				Color.TryParseHexString(hexColor, out transparent);
			}
			return transparent;
		}
	}

	private static Color gray;
	public static Color Gray
	{
		get{
			if(gray == null){
				gray = new Color(239f, 240f, 239f);
			}
			return gray;
		}
	}


	public void Reset()
	{
		this.colors = GetColors();
	}

	private List<Color> GetColors()
	{
		var allColors = GetAllColors ();
		var colors = new List<Color> ();

		for (int i = 0; i < size; ++i) {
			int index = Random.Range(0, allColors.Count);
			var color = allColors[index];
			allColors.RemoveAt(index);
			colors.Add(color);
		}

		return colors;
	}

	private List<Color> GetAllColors(){

		string[] hexColors = {"72dfc4", "e07065", "ffc65a", 
						      "ec7c91", "5cc0e1", "f1a470", 
							  "8796d3"};

		var allColors = new List<Color>();
		var color = Color.red;
		foreach (var hexColor in hexColors) {
			Color.TryParseHexString(hexColor, out color);
			allColors.Add (color);
		}

		return allColors;
	}

	public Color GetRandomColor(){
		var index = Random.Range (0, colors.Count);
		var color = colors [index];
		return color;
	}

	public Color GetRandomColorWithoutColors(List<Color> exludedColors){
		var validColors = new List<Color> (colors);
		foreach (var exColor in exludedColors) {
			validColors.Remove(exColor);
		}

		var color = GetRandomColorFrom(validColors);
		return color;
	}

	private Color GetRandomColorFrom(List<Color> randomColors){
		var index = Random.Range (0, randomColors.Count);
		var randomColor = randomColors [index];
		return randomColor;
	}



}
