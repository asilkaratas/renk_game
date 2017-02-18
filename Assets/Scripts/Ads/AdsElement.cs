using UnityEngine;
using System.Collections;

public interface AdsElement 
{
	void Request();
	void Show();
	void Hide();
	void Destroy();
	void Refresh();
}
