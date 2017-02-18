using UnityEngine;
using System.Collections.Generic;

public class Block : MonoBehaviour 
{
	public GameObject cellPrefab;
	public List<Cell> cells;

	public List<int> code;
	public Color color;
	public int size;

	void Start() 
	{
		size = Conf.Instance.BlockSize;
		cells = new List<Cell> ();

		var blockWidth = Conf.Instance.CalculateBlockWidthFromSize(size);
		var offset = blockWidth / 2;
		var boxCollider2d = GetComponent<BoxCollider2D> ();
		boxCollider2d.size = new Vector2 (blockWidth, blockWidth);

		InitCells();
	}

	private void InitCells() 
	{
		int totalSpace = Conf.Instance.TotalSpace;
		var posX = -Conf.Instance.CalculateBlockWidthFromSize (size-1)/2;
		var posY = -posX;

		for (var i = 0; i < size; ++i) 
		{
			for(var j = 0; j < size; ++j)
			{
				var index = i * size + j;

				var cellColor = ColorFactory.Gray;

				var cellInstance = GameObject.Instantiate(cellPrefab);

				cellInstance.transform.SetParent(transform);
				var pos = cellInstance.transform.localPosition;
				pos.x = j * totalSpace + posX;
				pos.y = -i * totalSpace + posY;
				pos.z = 0f;
				cellInstance.transform.localPosition = pos;
				
				var cell = cellInstance.GetComponent<Cell>();
				cell.Color = cellColor;
				cells.Add(cell);
			}
		}
	}

	public void Reset(List<int> code, Color color)
	{
		this.code = code;
		this.color = color;

		for (var i = 0; i < size; ++i) 
		{
			for(var j = 0; j < size; ++j)
			{
				var index = i * size + j;
				
				var cellColor = code[index] == 0 ? ColorFactory.Transparent : color;
				
				var cell = cells[index];
				cell.Color = cellColor;
			}
		}

		//Release();
	}

	public Cell GetCell(int index)
	{
		return cells [index];
	}

	public void Lock()
	{
		//cells.Clear ();
	}

	public void Grab() 
	{
		foreach (var cell in cells) 
		{
			LeanTween.scale(cell.gameObject, new Vector3(0.9f,0.9f,0.9f), 0.3f).setEase(LeanTweenType.easeOutBack);
		}

		//LeanTween.scale(gameObject, new Vector3(1f,1f,1f), 0.3f).setEase(LeanTweenType.easeOutBack);
	}

	public void Release() 
	{
		foreach (var cell in cells) 
		{
			LeanTween.scale(cell.gameObject, new Vector3(1f,1f,1f), 0.3f).setEase(LeanTweenType.easeOutBack);
		}

		//LeanTween.scale(gameObject, new Vector3(0.8f,0.8f,0.8f), 0.3f).setEase(LeanTweenType.easeOutBack);
	}

	void Update () 
	{
	
	}
}
