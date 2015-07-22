using UnityEngine;
using System.Collections;

public class RankIn : MonoBehaviour
{
	//A~Z , 0 ~ 9 , _ - ~ ! ? @
	private string[] chars = { "A", "B", "C", "D", "E", "F", "G", "H", "I", 
		"J", "K", "L", "M", "N", "O", "P", "Q", "R", 
		"S", "T", "U", "V", "W", "X", "Y", "Z", " "
	};
	public GameObject[] UnderBarObj;
	public GameObject[] CharObj;
	private int index = 0;
	private int[] charIndex;

	private int score = 0;

	private bool isDecide = false;

	int[] ScoreDatas = new int[5];
	string[] RankingName = new string[5];

	string RankName;

	void Start(){
		charIndex = new int[CharObj.Length];
		score = PlayerPrefs.GetInt ("CurrentScore", -1);

		for (int i = 1; i < 3; i++) {
			charIndex [i] = chars.Length - 1;
			CharObj [i].GetComponent<TextMesh>().text = chars [charIndex[i]];
		}

	}

	void Update ()
	{

		if (Input.anyKeyDown && Input.GetKeyDown (KeyCode.Z) && index == 3 && isDecide == false) {
			DecideKey ();
		
		}

		if (Input.anyKeyDown) {
		
			float v = Input.GetAxis ("Vertical");

			if (0 < v) {
				DownIndex ();
			} else if (0 > v) {
				UpIndex ();
			}

			if (Input.GetKeyDown (KeyCode.X)) {
				if (index > 0) {
					RightShift ();
				}
			}

			if (Input.GetKeyDown (KeyCode.Z)) {
				if (index < 3) {
					LeftShift ();
				}
			}

		}

		if(index != 3)
			CharObj [index].GetComponent<TextMesh>().text = chars [charIndex[index]];
	
		CharObj [index].GetComponent<Animator> ().SetBool ("isTenmetsu", true);
		UnderBarObj [index].GetComponent<Animator> ().SetBool ("isTenmetsu", true);

		InitObj ();
	}

	void InitObj(){

		for (int i = 0; i < CharObj.Length; i++) {

			if (i != index) {
				CharObj [i].GetComponent<Animator> ().SetBool ("isTenmetsu", false);
				UnderBarObj [i].GetComponent<Animator> ().SetBool ("isTenmetsu", false);
			}
		}

	}

	void DecideKey(){

		RankName = CharObj [0].GetComponent<TextMesh>().text + 
		              CharObj [1].GetComponent<TextMesh>().text + 
		              CharObj [2].GetComponent<TextMesh>().text;
		              
		for(int i = 0; i < 5; i++) {

			if (PlayerPrefs.HasKey ("Ranking_" + (i + 1) + "_Score")) {
				ScoreDatas [i] = PlayerPrefs.GetInt ("Ranking_" + (i + 1) + "_Score");
				RankingName [i] = PlayerPrefs.GetString ("Ranking_" + (i + 1) + "_Name");
			}
			else
				break;
		}

		InputRanking ();

		for (int i = 0; i < 5; i++) {
			PlayerPrefs.SetInt ("Ranking_" + (i + 1) + "_Score", ScoreDatas[i]);
			PlayerPrefs.SetString ("Ranking_" + (i + 1) + "_Name", RankingName [i]);

		}
			
		PlayerPrefs.Save ();

		FadeManager.Instance.LoadLevel ("Ranking", 1.0f, _FadeColor.Black);

		isDecide = true;
	}

	void InputRanking(){

		for (int i = 0; i < 5; i++) {

			if (ScoreDatas [i] < score) {
				int tmpScore = ScoreDatas [i];
				ScoreDatas [i] = score;

				string tmpname = RankingName [i];
				RankingName [i] = RankName;

				for (int j = i + 1; j < 5; j++) {
					int tmpScoreJ = ScoreDatas [j];
					string tmpnameJ = RankingName [j];
						
					if (j == i + 1) {
						ScoreDatas [j] = tmpScore;
						RankingName [j] = tmpname;
					} else {
						ScoreDatas [j] = tmpScoreJ;
						RankingName [j] = tmpnameJ;
					}

				}

				return;

			}

		}
	}

	void LeftShift(){
		index = (index + 1) % CharObj.Length;
	}

	void RightShift(){
		index -= 1;

		if (index < 0) {
			index = (index + CharObj.Length) % CharObj.Length;
		}
	}

	void UpIndex ()
	{
		charIndex[index] = (charIndex[index] + 1) % chars.Length;
	}

	void DownIndex ()
	{

		charIndex[index] -= 1;

		if (charIndex[index] < 0)
			charIndex[index] = (charIndex[index] + chars.Length) % chars.Length;
	}
}
