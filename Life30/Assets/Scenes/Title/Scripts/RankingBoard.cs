using UnityEngine;
using System.Collections;

public class RankingBoard : MonoBehaviour {

	public TextMesh[] TextMeshObj;

	// Use this for initialization
	void Start () {

		string[] Name = new string[5];
		int[] score = new int[5];

		for (int i = 0; i < 5; i++) {

			score[i] = PlayerPrefs.GetInt ("Ranking_" + (i + 1) + "_Score");
			Name[i] = PlayerPrefs.GetString ("Ranking_" + (i + 1) + "_Name");

			//Debug.Log ("Name: " + Name[i] + "Score: " + score [i]);


			TextMeshObj [i].text = (i + 1).ToString() + ".  " + Name [i] + "  " + score [i];
		}



	}
		
}
