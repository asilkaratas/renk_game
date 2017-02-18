using UnityEngine;
using System.Collections;

public class WallCell : Cell 
{
	public BoardCell BoardCell { get; set; }


	public bool HasValidBoardCell{
		get{
			return BoardCell && BoardCell.Cell && BoardCell.Cell.Color == Color;
		}
	}

	void Start(){

		base.Start ();
		Reset ();
	}

	public void Reset(){
		var localScale = transform.localScale;
		if (Orientation == CellOrientation.Left || Orientation == CellOrientation.Right) {
			localScale.x = 0.5f;
		} else if(Orientation == CellOrientation.Top || Orientation == CellOrientation.Bottom)  {
			localScale.y = 0.5f;
		}
		transform.localScale = localScale;
	}

	public void Open(){
		if (Orientation == CellOrientation.Left || Orientation == CellOrientation.Right) {
			LeanTween.scaleX(gameObject, 1f, 0.3f).setEase(LeanTweenType.easeOutBack);
		} else {
			LeanTween.scaleY(gameObject, 1f, 0.3f).setEase(LeanTweenType.easeOutBack);
		}
	}

	public void Close(){
		if (Orientation == CellOrientation.Left || Orientation == CellOrientation.Right) {
			LeanTween.scaleX(gameObject, 0.5f, 0.3f).setEase(LeanTweenType.easeOutBack);
		} else {
			LeanTween.scaleY(gameObject, 0.5f, 0.3f).setEase(LeanTweenType.easeOutBack);
		}
	}
}
