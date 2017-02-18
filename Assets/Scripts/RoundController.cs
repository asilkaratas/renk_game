using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using StateManager;

public class RoundController : MonoBehaviour {

	private Board board;
	private BlockFactory blockFactory;
	private List<Block> blocks = new List<Block> ();

	private int count;
	private bool solving;
	private SoundManager soundManager;

	private bool firstRound = true;

	void Start () 
	{
		board = GetComponent<Board> ();
		board.OpenCompletedCallback += OnOpenCompletedHandler;
		blockFactory = GetComponent<BlockFactory>();

		soundManager = board.soundManager;

		NextRound ();

	}

	private void OnOpenCompletedHandler ()
	{
		Debug.Log("OnOpenCompletedHandler:" + count);

		solving = false;

		if (count == 0) 
		{
			NextRound();
		}
		else 
		{
			CheckHasMove();
		}
	}

	private void CheckHasMove()
	{
		if(!board.HasMove(blocks))
		{
			GameStateManager gameStateManager = FindObjectOfType<GameStateManager> ();
			gameStateManager.GoToGameEnd ();
		}
	}

	public void Reset()
	{
		for(int i = 0; i < blocks.Count; ++i)
		{
			var block = blocks[i];
			var dragController = block.GetComponent<DragController> ();
			dragController.DropCallback -= OnDropHandler;

			block.gameObject.SetActive(false);
		}
	}

	public void Restart()
	{
		firstRound = true;

		Reset ();
		NextRound ();
	}
	
	private void NextRound() 
	{
		StartCoroutine (NextRoundDelayed());
	}

	private IEnumerator NextRoundDelayed()
	{
		yield return new WaitForSeconds (0.5f);

		ShowNexRound ();
	}

	private void ShowNexRound(){
		Reset ();

		var blockCount = Conf.Instance.BlockCount;
		blocks = blockFactory.GetRandomBlocks ();
		
		var positions = new float[]{-100f, 0f, 100f};
		
		
		for (var i = 0; i < blockCount; ++i) {
			var block = blocks[i];
			var blockInstance = block.gameObject;
			blockInstance.SetActive(true);

			var pos = blockInstance.transform.localPosition;
			pos.x = positions[i] + Screen.width;
			pos.y = -180f;
			pos.z = -2;
			blockInstance.transform.localPosition = pos;
			
			LeanTween.moveLocal(block.gameObject, new Vector3(positions[i], pos.y, pos.z), 0.3f)
				.setEase(LeanTweenType.easeOutExpo)
					.setOnComplete(()=>{
						var localPos = block.transform.localPosition;
						localPos.x = Mathf.Round(localPos.x);
						block.transform.localPosition = localPos;
					});
			
			var dragController = blockInstance.GetComponent<DragController>();
			dragController.DropCallback += OnDropHandler;
		}
		
		this.count = blockCount;

		if(!firstRound)
		{
			soundManager.PlayNewRoundSound();
		}
		firstRound = false;

		CheckHasMove();
	}

	private bool OnDropHandler(Block block) 
	{
		if(solving)
			return false;

		var hasPosition = board.HasPosition (block);
		if (hasPosition) 
		{
			solving = true;

			soundManager.PlayPlacedSound();

			board.gameScreenLogger.LogSuccessPlacing();

			var dragController = block.GetComponent<DragController>();
			dragController.DropCallback -= OnDropHandler;

			count --;
		}
		else
		{
			soundManager.PlayNotPlacedSound();
			board.gameScreenLogger.LogFailPlacing();
		}

		return hasPosition;
	}
}
