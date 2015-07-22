using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndIdle : State
{

	private GameObject RankIn;
	private bool isRankIn = false;

	public override void StateStart ()
	{
		Debug.Log (this.ToString () + " Start!");

		Text label1 = GameObject.Find ("Label1").GetComponent<Text> ();
		int score = PlayerPrefs.GetInt ("CurrentScore");
		label1.text = score + "秒間";

		int[] ScoreDatas = new int[5];

		for(int i = 0; i < 5; i++) {

			if (PlayerPrefs.HasKey ("Ranking_" + (i + 1) + "_Score")) {
				ScoreDatas [i] = PlayerPrefs.GetInt ("Ranking_" + (i + 1) + "_Score", -1);
			} else {
				ScoreDatas [i] = -1;
			}
			
		}
			
		for (int i = 0; i < 5; i++) {
			if (ScoreDatas [i] <= score) {
				isRankIn = true;
			}
		}

		if(isRankIn == true)
			RankIn = this.ResourceManagerInstance.InstantiateResourceWithName ("RankIn");

		SoundManager.Instance.PlayBGM ("EndBGM");
	}

	public override void StateUpdate ()
	{

		if (Input.anyKey && isRankIn == false) {
			FadeManager.Instance.LoadLevel ("Title", 1.0f, _FadeColor.Black);
		}
	
	}

	public override void StateDestroy ()
	{
		Debug.Log (this.ToString () + " Destroy!");
	}
}
