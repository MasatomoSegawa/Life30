using UnityEngine;
using System.Collections;

public class EndInput : MonoBehaviour {

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.Escape)){
			Application.Quit();
		}

	}
}
