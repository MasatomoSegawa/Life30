using UnityEngine;
using System.Collections;

public class TitleManager : MonoBehaviour {

	bool isEnd = false;

	void Start(){
		this.InitScore ();
		SoundManager.Instance.PlayBGM ("Title");
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

	public void OnStartButtonClick(){
		if(isEnd == false){
			FadeManager.Instance.LoadLevel ("GamePlay", 2.0f, _FadeColor.Red);
			isEnd = true;
		}
	}

	public void OnRankingButtonClick(){
		if (isEnd == false) {
			FadeManager.Instance.LoadLevel ("Ranking", 2.0f, _FadeColor.Red);
			isEnd = true;
		}
	}

}
