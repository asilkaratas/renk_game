using UnityEngine;
using System.Collections;



[RequireComponent(typeof(BoxCollider2D))]
public class DragController : MonoBehaviour {

	private Block block;
	private Vector3 originalPos;
	private Vector3 startPos;
	private float offset;

	public delegate bool OnDrop(Block block);
	public event OnDrop DropCallback;

	void Start () {
		block = GetComponent<Block> ();

	}

	void Update () {
	
	}

	void OnMouseDown(){

		//Debug.Log ("OnMouseDown");
		originalPos = block.transform.localPosition;

		Vector3 mousePosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0);
		startPos = Camera.main.ScreenToWorldPoint (mousePosition)-transform.position;

		block.Grab ();
		//LeanTween.move( block.container, new Vector3(0f, 20f, 0f), 0.3f).setEase(LeanTweenType.easeOutQuad);

		LeanTween.value( gameObject, 0f, 50f, 0.2f).setOnUpdate( (float val)=>{ 
			//Debug.Log("tweened val:"+val);
			offset = val;

			OnMouseDrag();
		} ).setEase(LeanTweenType.easeOutQuad);

	}

	void OnMouseDrag(){

		//Debug.Log ("OnMouseDrag");
		Vector3 mousePosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0);
		Vector3 pos = Camera.main.ScreenToWorldPoint (mousePosition) - startPos;

		pos.y += offset;
		pos.z = transform.localPosition.z;
		pos.x = (int)pos.x;
		pos.y = (int)pos.y;

		transform.localPosition = pos;

		//LeanTween.cancel(gameObject);
		//LeanTween.moveLocal(gameObject, pos, 0.3f).setEase(LeanTweenType.easeOutQuad);
	}

	void OnMouseUp(){

		//Debug.Log ("OnMouseUp");

		block.Release ();

		if (DropCallback != null) {
			var hasPosition = DropCallback(block);
			if(!hasPosition){
				LeanTween.move( block.gameObject, originalPos, 0.3f).setEase(LeanTweenType.easeOutQuad);
			}
		}
	}
}
