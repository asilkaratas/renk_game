using UnityEngine;
using System.Collections;

public class Cell : MonoBehaviour
{
	public enum CellOrientation{
		Left,
		Top,
		Right,
		Bottom,
		Center
	}

	public Sprite left;
	public Sprite top;
	public Sprite right;
	public Sprite bottom;
	public Sprite center;

	private Color color;
	public Color Color {
		get{
			return color;
		}

		set{
			color = value;

			var renderer = GetComponent<SpriteRenderer> ();
			renderer.color = color;
		}
	}

	public bool Marked;
	public CellOrientation Orientation = CellOrientation.Center;
	
	public void Start ()
	{
		var renderer = GetComponent<SpriteRenderer> ();
		renderer.sprite = GetSprite ();
	}

	private Sprite GetSprite(){
		if (Orientation == CellOrientation.Left) {
			return left;
		}

		if (Orientation == CellOrientation.Top) {
			return top;
		}

		if (Orientation == CellOrientation.Right) {
			return right;
		}

		if(Orientation == CellOrientation.Bottom){
			return bottom;
		}

		return center;
	}

}
