using UnityEngine;
using System.Collections;

public class AccidentInfo : MonoBehaviour {

	private string[] text1string = {"error",
		"超常現象発生！","動機が激しくなってきた！","中毒が悪化！","頭がいたい！", "目が霞んできた！"
	};
	private string[] text2string = {"error",
		"ゆっくり振るほどたくさん出る！" , "ビンを振る手が落ち着かない!",  "薬の副作用が強くなる！", "薬を飲まないとフラフラする！","手のひらに出している薬がよく見えない！"
	};

	public void init(int id){

		Debug.Log ("id:" + id);
		Debug.Log (text1string[id]);
		Debug.Log (text2string [id]);

		this.transform.GetChild (0).GetComponent<TextMesh> ().text = text1string [id];
		this.transform.GetChild (1).GetComponent<TextMesh> ().text = text2string [id];

	
	}

}
