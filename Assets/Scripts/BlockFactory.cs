using UnityEngine;
using System.Collections.Generic;

public class BlockFactory : MonoBehaviour 
{

	public GameObject blockPrefab;

	private ColorFactory colorFactory;
	private List<List<int>> blockCodes;
	private List<Block> blocks = new List<Block>();

	void Start () 
	{
		Debug.Log("blockPrefab:::" + blockPrefab);
		colorFactory = GetComponent<ColorFactory> ();

		this.blockCodes = new List<List<int>>{
			BlockCode.L1, BlockCode.L2, BlockCode.L3, BlockCode.L4,
			BlockCode.LL1, BlockCode.LL2, BlockCode.LL3, BlockCode.LL4,
			BlockCode.LLL1, BlockCode.LLL2, BlockCode.LLL3, BlockCode.LLL4,
			BlockCode.V2, BlockCode.V3, BlockCode.V4,
			BlockCode.H2, BlockCode.H3, BlockCode.H4,
			BlockCode.CUBE1, BlockCode.CUBE1, BlockCode.CUBE1,
			BlockCode.CUBE2, BlockCode.CUBE3};

		InitBlocks();
	}

	private void InitBlocks()
	{
		int blockCount = Conf.Instance.BlockCount;
		//Debug.Log("blockPrefab:" + blockPrefab + " blockCount:" + blockCount);
		for(int i = 0; i < blockCount; ++i)
		{
			var blockInstance = GameObject.Instantiate(blockPrefab);
			var block = blockInstance.GetComponent<Block>();

			blocks.Add(block);
		}
	}

	public List<Block> GetRandomBlocks()
	{
		var validCodes = new List<List<int>>(blockCodes);
		for (var i = 0; i < blocks.Count; ++i) 
		{
			var index = Random.Range(0, validCodes.Count);

			var code = validCodes[index];
			RemoveAll(validCodes, code);

			var color = colorFactory.GetRandomColor();

			var block = blocks[i];
			block.Reset(code, color);
		}

		return blocks;
	}

	private void RemoveAll(List<List<int>> codes, List<int> code)
	{
		for (var i = 0; i < codes.Count; ++i)
		{
			if(codes[i] == code)
			{
				codes.RemoveAt(i);
			}
		}
	}

}
