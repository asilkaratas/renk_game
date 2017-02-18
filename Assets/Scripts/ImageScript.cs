using UnityEngine;
using System.Collections;

public class ImageScript : MonoBehaviour {

	public GameObject imagePrefab;

	// Use this for initialization
	void Start () {
		var instance = GameObject.Instantiate (imagePrefab);
		var pos = instance.transform.position;
		pos.x = 100;
		instance.transform.position = pos;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
