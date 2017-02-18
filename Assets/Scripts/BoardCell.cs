using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardCell : Cell 
{
	//private AudioSource audioSource;

	void Awake()
	{
		cell = GetComponentsInChildren<Cell>()[1];
		cell.Color = ColorFactory.Transparent;

		//audioSource = GetComponent<AudioSource>();
	}

	private List<BoardCell> neighbors = new List<BoardCell>();
	public List<BoardCell> Neighbors 
	{
		get
		{
			return neighbors;
		}
	}

	public void AddNeighbor(BoardCell neighbor){
		neighbors.Add (neighbor);
	}

	public bool HasValidCell()
	{
		return CellColor != ColorFactory.Transparent;
	}

	private Cell cell;
	public Cell Cell 
	{ 
		get
		{
			return cell;
		}
	}

	public Color CellColor
	{
		get
		{
			return cell.Color;
		}

		set
		{
			cell.Color = value;
		}
	}

	public void Reset()
	{
		CellColor = ColorFactory.Transparent;
		cell.gameObject.transform.localScale = new Vector3(1, 1, 1);
	}

	private List<WallCell> wallCells = new List<WallCell> ();
	public List<WallCell> WallCells 
	{ 
		get
		{
			return wallCells;
		} 
	}

	public void AddWallCell(WallCell wallCell)
	{
		wallCells.Add (wallCell);
	}

	public bool HasValidWallCell(WallCell wallCell)
	{
		var hasValid = false;
		foreach (var cell in wallCells) 
		{
			if(cell != wallCell && cell.Color == wallCell.Color)
			{
				cell.Marked = true;
				cell.Open();
				hasValid = true;
			}
		}

		return hasValid;
	}

	public void CloseWallCells()
	{
		foreach (var wallCell in wallCells) 
		{
			wallCell.Close();
		}
	}

	/*
	public void PlaySound(AudioClip clip)
	{
		audioSource.clip = clip;
		audioSource.Play();
	}
	*/


}
