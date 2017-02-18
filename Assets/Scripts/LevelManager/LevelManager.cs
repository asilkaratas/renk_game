using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour 
{
	private RenkStore renkStore;

	public Button[] buttons;

	private Button selectedButton = null;
	private Button SelectedButton
	{
		get
		{
			return selectedButton;
		}

		set
		{
			if(selectedButton == value)return;

			if(selectedButton != null)
			{
				selectedButton.image.color = Color.gray;
			}

			selectedButton = value;

			if(selectedButton != null)
			{
				selectedButton.image.color = Color.white;
			}
		}
	}

	private void OnLevelChanged(int level)
	{
		SelectedButton = buttons[level];
	}

	void Start () 
	{
		renkStore = GetComponent<RenkStore>();

		for(int i = 0; i < buttons.Length; ++i)
		{
			var button = buttons[i];
			var index = i;
			button.onClick.AddListener(()=>
			{
				if(index == 1 && !renkStore.HasRenk5())
				{
					renkStore.BuyRenk5();
					return;
				}else if(index == 2 && !renkStore.HasRenk6())
				{
					renkStore.BuyRenk6();
					return;
				}

				GameModel.Instance.Level = index;
			});

			button.image.color = Color.gray;

		}

		var level = GameModel.Instance.Level;
		SelectedButton = buttons[level];

		GameModel.Instance.LevelCallback += OnLevelChanged;
	}
}
