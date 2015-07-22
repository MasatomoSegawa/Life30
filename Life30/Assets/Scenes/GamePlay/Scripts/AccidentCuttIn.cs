using UnityEngine;
using System.Collections;

public class AccidentCuttIn : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		GameModel.isFreeze = true;
		this.GetComponent<Animator> ().SetTrigger ("doAccidentCuttIn");

	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void End(){
		GameModel.isFreeze = false;
		Destroy (this.gameObject);
	}
}
