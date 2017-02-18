using UnityEngine;
using System.Collections;

public class Conf 
{
	public int Size;

	private int boardSize;
	public int BoardSize 
	{
		get 
		{
			return boardSize;
		}
	}

	private int cellSize;
	public int CellSize
	{
		get
		{
			return cellSize;
		}
	}

	private int blockSize;
	public int BlockSize
	{
		get
		{
			return blockSize;
		}
	}

	private Color cellColor;
	public Color CellColor
	{
		get
		{
			return cellColor;
		}
	}

	private int cellSpace;
	public int CellSpace
	{
		get
		{
			return cellSpace;
		}
	}

	private int totalSpace;
	public int TotalSpace
	{
		get
		{
			return totalSpace;
		}
	}

	private int blockCount;
	public int BlockCount
	{
		get
		{
			return blockCount;
		}
	}

	public int CalculateBlockWidthFromSize(int size){
		//var width = size * cellSize + (size - 1) * cellSpace;
		var width = size * (cellSize + cellSpace);
		return width;
	}

	private static Conf instance;
	public static Conf Instance
	{
		get
		{
			if (instance == null) 
			{
				instance = new Conf();
			}
			return instance;
		}
	}


	private Conf()
	{
		this.boardSize = 10;
		this.cellSize = 20;
		this.cellColor = Color.gray;
		this.cellSpace = 1;
		this.blockCount = 3;
		this.blockSize = 4;

		this.totalSpace = cellSize + cellSpace;

	}


	

	
	

	
	



}
