using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class Board : MonoBehaviour 
{
	private int size = Conf.Instance.BoardSize;
	private BoardCell[] boardCells = new BoardCell[Conf.Instance.BoardSize * Conf.Instance.BoardSize];
	private WallCell[] wallCells = new WallCell[Conf.Instance.BoardSize * 4];

	public GameObject boardCellPrefab;
	public GameObject wallCellPrefab;

	private ColorFactory colorFactory;
	private WallColorFactory wallColorFactory;

	private ToneManager toneManager;
	private ScoreCalculator scoreCalculator;
	private RoundController roundController;

	public SoundManager soundManager;
	public GameScreenLogger gameScreenLogger;

	public delegate void OnOpenCompleted();
	public event OnOpenCompleted OpenCompletedCallback;

	void Awake()
	{
		colorFactory = GetComponent<ColorFactory>();
		wallColorFactory = GetComponent<WallColorFactory>();
		initBoardCells();
		initWallCells();

		Vector2 pos = transform.localPosition;
		pos.y = 5;
		transform.localPosition = pos;

		roundController = GetComponent<RoundController> ();
		toneManager = GetComponent<ToneManager> ();
		scoreCalculator = GetComponent<ScoreCalculator>();
	}

	private int GetSizeFromLevel()
	{
		var level = GameModel.Instance.Level;
		var size = level + 4;
		return size;
	}

	public void Reset()
	{
		Debug.Log ("Board.Restart:" + Time.time);

		colorFactory.Size = GetSizeFromLevel();
		//colorFactory.Reset ();
		
		foreach (var boardCell in boardCells) 
		{
			boardCell.Reset();
		}

		var wallColors = wallColorFactory.GetWallColors (size);
		for (var i = 0; i < wallColors.Count; ++i)
		{
			var wallCell = wallCells[i];
			wallCell.Color = wallColors[i];
			wallCell.Reset();
		}

		roundController.Reset ();
		
		GameModel.Instance.Reset ();
	}

	public void Restart()
	{
		Reset ();


		roundController.Restart ();
	}

	private void initBoardCells()
	{
		int totalSpace = Conf.Instance.TotalSpace;
		int halfSize = size / 2;
		int startPosX = -Conf.Instance.CalculateBlockWidthFromSize (size-1)/2;
		var startPosY = -startPosX;
		var index = 0;
		
		for (int i = 0; i < size; ++i) {
			for(int j = 0; j < size; ++j){
				var boardCellInstance = GameObject.Instantiate(boardCellPrefab);
				var boardCell = boardCellInstance.GetComponent<BoardCell>();

				boardCell.transform.SetParent(transform);

				Vector2 pos = boardCell.transform.localPosition;
				pos.x = j * totalSpace + startPosX;
				pos.y = -i * totalSpace + startPosY;

				var leftIndex = j - 1;
				var topIndex = i - 1;

				index = i * size + j;

				if(leftIndex >= 0){
					var leftCellIndex = i * size + leftIndex;
					var leftCell = boardCells[leftCellIndex];
					leftCell.AddNeighbor(boardCell);
					boardCell.AddNeighbor(leftCell);
				}

				if(topIndex >= 0){
					var topCellIndex = topIndex * size + j;
					var topCell = boardCells[topCellIndex];
					topCell.AddNeighbor(boardCell);
					boardCell.AddNeighbor(topCell);
				}
				
				boardCell.transform.localPosition = pos;

				boardCells[index] = boardCell;
			}
		}
	}

	private void initWallCells()
	{
		var cellSize = Conf.Instance.CellSize;
		var cellSpace = Conf.Instance.CellSpace;
		var totalSpace = Conf.Instance.TotalSpace;

		var startPosX = -Conf.Instance.CalculateBlockWidthFromSize (size-1)/2;
		var startPosY = -startPosX;
		var leftPos = startPosX - totalSpace + cellSize / 2;
		var topPos = startPosY + totalSpace - cellSize / 2;
		var rightPos = -startPosX + totalSpace + cellSpace - cellSize/2;
		var bottomPos = -startPosY - totalSpace - cellSpace + cellSize/2;

		for (var i = 0; i < size; ++i) 
		{
			BoardCell boardCell;
			Vector2 pos;


			//left
			var leftCellInstance = GameObject.Instantiate(wallCellPrefab);
			leftCellInstance.transform.SetParent(transform);
			pos = leftCellInstance.transform.localPosition;
			pos.x = leftPos ;
			pos.y = -i * totalSpace + startPosY;
			leftCellInstance.transform.localPosition = pos;

			var leftCell = leftCellInstance.GetComponent<WallCell>();
			leftCell.Orientation = Cell.CellOrientation.Right;
			boardCell = boardCells[i * size];
			boardCell.AddWallCell(leftCell);
			leftCell.BoardCell = boardCell;

			wallCells[size - 1 - i] = leftCell;

			//top
			var topCellInstance = GameObject.Instantiate(wallCellPrefab);
			topCellInstance.transform.SetParent(transform);
			pos = topCellInstance.transform.localPosition;
			pos.x = i * totalSpace + startPosX;
			pos.y = topPos;
			topCellInstance.transform.localPosition = pos;

			var topCell = topCellInstance.GetComponent<WallCell>();
			topCell.Orientation = Cell.CellOrientation.Bottom;
			boardCell = boardCells[i];
			boardCell.AddWallCell(topCell);
			topCell.BoardCell = boardCell;

			wallCells[size + i] = topCell;


			//right
			var rightCellInstance = GameObject.Instantiate(wallCellPrefab);
			rightCellInstance.transform.SetParent(transform);
			pos = rightCellInstance.transform.localPosition;
			pos.x = rightPos;
			pos.y = -i * totalSpace + startPosY;
			rightCellInstance.transform.localPosition = pos;

			var rightCell = rightCellInstance.GetComponent<WallCell>();
			rightCell.Orientation = Cell.CellOrientation.Left;
			boardCell = boardCells[(i+1) * size - 1];
			boardCell.AddWallCell(rightCell);
			rightCell.BoardCell = boardCell;

			wallCells[size * 2 + i] = rightCell;


			//bottom
			var bottomCellInstance = GameObject.Instantiate(wallCellPrefab);
			bottomCellInstance.transform.SetParent(transform);
			pos = bottomCellInstance.transform.localPosition;
			pos.x = i * totalSpace + startPosX;
			pos.y = bottomPos;
			bottomCellInstance.transform.localPosition = pos;

			var bottomCell = bottomCellInstance.GetComponent<WallCell>();
			bottomCell.Orientation = Cell.CellOrientation.Top;
			boardCell = boardCells[size * (size - 1) + i];
			boardCell.AddWallCell(bottomCell);
			bottomCell.BoardCell = boardCell;

			wallCells[size * 3 + size - 1 - i] = bottomCell;
		}
	}

	private bool HasBlockFix(Block block, int leftIndex, int topIndex)
	{
		var blockSize = block.size;
		var blockCode = block.code;
		var maxBoardCellIndex = boardCells.Length - 1;
		
		for (var i = 0; i < blockSize; ++i) {
			for (var j = 0; j < blockSize; ++j) {
				var blockCellIndex = i * blockSize + j;
				var boardCellIndex = (i + topIndex) * size + (leftIndex + j);
				
				if(boardCellIndex >= 0 && boardCellIndex <= maxBoardCellIndex && (leftIndex + j) < size && (leftIndex + j) >= 0){
					var boardCell = boardCells [boardCellIndex];
					
					if (blockCode [blockCellIndex] == 1 && boardCell.HasValidCell()) {
						return false;
					}
				}else if(blockCode [blockCellIndex] == 1){
					return false;
				}
				
			}
		}

		return true;
	}

	public bool HasPosition(Block block)
	{
		var blockWidth = Conf.Instance.CalculateBlockWidthFromSize (block.size);
		var boardWidth = Conf.Instance.CalculateBlockWidthFromSize (size);
		var totalSpace = Conf.Instance.TotalSpace;

		var blockLeft = block.transform.position.x - blockWidth / 2 + boardWidth / 2;
		var blockTop = -(block.transform.position.y + blockWidth / 2 - transform.localPosition.y) + boardWidth / 2;

		var leftIndex = Mathf.RoundToInt (blockLeft / totalSpace);
		var topIndex = Mathf.RoundToInt (blockTop / totalSpace);

		//Debug.Log (" leftIndex:" + leftIndex + " topIndex:" + topIndex);

		var hasPosition = HasBlockFix (block, leftIndex, topIndex);

		//Debug.Log ("hasPosition:" + hasPosition + " leftIndex:" + leftIndex + " topIndex:" + topIndex  + " " + Time.time);

		if (hasPosition) {
			var posX = leftIndex * totalSpace - boardWidth/2 + blockWidth/2;
			var posY = -topIndex * totalSpace + boardWidth/2 - blockWidth/2 + transform.localPosition.y;
			var posZ = block.transform.position.z;

			//Debug.Log ("totalSpace:" + totalSpace + " boardWidth:" + boardWidth + " blockWidth:" + blockWidth  + " " + Time.time);

			LeanTween.move(block.gameObject, new Vector3(posX, posY, posZ), 0.2f).setEase(LeanTweenType.easeOutExpo).setOnComplete(
				()=>{
				ChangeContainer(block, leftIndex, topIndex);
			});

			block.Lock ();
		}

		return hasPosition;
	}

	private void ChangeContainer(Block block, int leftIndex, int topIndex){

		//Debug.Log ("ChangeContainer:" + block + " leftIndex:" + leftIndex + " topIndex:" + topIndex); 

		var blockSize = block.size;
		var blockCode = block.code;

		for (var i = 0; i < blockSize; ++i) {
			for(var j = 0; j < blockSize; ++j){
				var blockCellIndex = i * blockSize + j;
				var boardCellIndex = (i + topIndex) * size + (leftIndex + j);


				if(blockCode[blockCellIndex] != 0){

					var cell = block.GetCell(blockCellIndex);
					var boardCell = boardCells[boardCellIndex];
					//Debug.Log("here:" + blockCellIndex + " :" + cell);
					//boardCell.Cell = cell;
					boardCell.CellColor = block.color;
				}
			}
		}

		//block.transform.SetParent (null);
		//Destroy(block.gameObject);
		block.gameObject.SetActive(false);

		Solve ();
	}

	private void MarkAllCells(){
		foreach (var cell in boardCells) {
			cell.Marked = false;
		}
	}

	private void MarkAllWallCells(){
		foreach (var cell in wallCells) {
			cell.Marked = false;
		}
	}

	private void Solve(){
		MarkAllWallCells ();

		for (var i = 0; i < wallCells.Length; ++i) 
		{
			var wallCell = wallCells[i];
			var hasMatch = false;

			MarkAllCells();

			var visited = new List<BoardCell>();

			if(!wallCell.Marked && wallCell.HasValidBoardCell){

				wallCell.Open();

				var boardCell = wallCell.BoardCell;
				var visiting = new List<BoardCell>{boardCell};

				while(visiting.Count > 0){
					boardCell = visiting[0];
					visiting.RemoveAt(0);
					visited.Add(boardCell);

					if(boardCell.HasValidWallCell(wallCell)){
						hasMatch = true;
					}

					var neighbors = boardCell.Neighbors;
					for(var j = 0; j < neighbors.Count; ++j){
						var neighbor = neighbors[j];
						if(neighbor.Cell && neighbor.Cell.Color == wallCell.Color &&
						   visited.IndexOf(neighbor) == -1 && visiting.IndexOf(neighbor) == -1){
							visiting.Add(neighbor);
						}
					}
				}
			}

			if(hasMatch)
			{
				gameScreenLogger.LogSolvedCount(visited.Count);

				LeanTween.delayedCall(0.5f, ()=>
				{
					ShowSolveList(visited);
				});

				return;
			}
		}

		OpenCompletedCallback();
	}

	private void ShowSolveList(List<BoardCell> visited)
	{

		List<AudioClip> sounds = toneManager.GetRandomSounds (visited.Count);
		Debug.Log ("sounds:" + sounds.Count);
		var delayMultiplier = 0.128f;
		//var delayMultiplier = 0.02f;
		for(var i = 0; i < visited.Count; ++i){
			var boardCell = visited[i];

			boardCell.CloseWallCells();

			var delay = i * delayMultiplier;
			var index = i;

			LeanTween.alpha(boardCell.Cell.gameObject, 0f, 0.4f).setDelay(delay).setEase(LeanTweenType.easeOutSine);
			LeanTween.scale(boardCell.Cell.gameObject, new Vector3(0.5f,0.5f,0.5f), 0.5f)
				.setDelay(delay)
				.setEase(LeanTweenType.easeOutSine)
				.setOnComplete(()=>{
					//boardCell.Cell = null;
						boardCell.Reset();

			});

			LeanTween.delayedCall(delay, ()=>{
				AudioClip sound = sounds[index];
				//boardCell.PlaySound(sound);
				toneManager.PlaySound(sound);
			});

		}

		var delayForCall = visited.Count * delayMultiplier;
		LeanTween.delayedCall(delayForCall, ()=>{
			OpenCompletedCallback();
		});

		var count = visited.Count;
		var score = scoreCalculator.GetScore(count);
		GameModel.Instance.AddScore(score);
	}

	public bool HasMove(List<Block> blocks){
		var boardSize = size;

		foreach (var block in blocks) {
			var blockCode = block.code;
			var minIndex = -block.size;
			var maxIndex = boardSize + block.size;

			if(block.gameObject.activeSelf)
			{
				for(var i = minIndex; i < maxIndex; ++i){
					for(var j = minIndex; j < maxIndex; ++j){
						if(HasBlockFix(block, i, j)){
							return true;
						}
					}
				}
			}
		}

		return false;
	}

}
