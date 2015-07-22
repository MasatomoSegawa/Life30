using UnityEngine;
using System.Collections;

public class TitleIdle : State
{

	private GameObject TitleGUIs;
	private ButtonController buttonScript;

	public override void StateStart ()
	{
		Debug.Log (this.ToString () + " Start!");

		/*
		TitleGUIs = GameObject.Find ("TitleGUIs");
		buttonScript = TitleGUIs.GetComponent<ButtonController> ();

		buttonScript.InputStartButtonEvent += () => {
			buttonScript.OnFreeze();
			FadeManager.Instance.LoadLevel("GamePlay",0.5f,_FadeColor.Black);
		};

		buttonScript.InputRankingButtonEvent += () => {
			buttonScript.OnFreeze();
			FadeManager.Instance.LoadLevel("Ranking",1.0f,_FadeColor.Black);

		};
		*/

		InitScore ();
	}

	public override void StateUpdate ()
	{

		/*
		if (Input.GetKeyDown (KeyCode.C)) {
			PlayerPrefs.DeleteAll ();
		}
		*/
		
	}

	public override void StateDestroy ()
	{
		Debug.Log (this.ToString () + " Destroy!");
	}



	void InitScore(){
	
		for (int i = 0; i < 5; i++) {

			if (PlayerPrefs.HasKey ("Ranking_" + (i + 1) + "_Score") == false) {
				PlayerPrefs.SetInt ("Ranking_" + (i + 1) + "_Score", 10);
				PlayerPrefs.SetString ("Ranking_" + (i + 1) + "_Name", "TAS");
				PlayerPrefs.Save ();
			}

		}

	}

}
