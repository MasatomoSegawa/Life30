using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PreYakuNumText : MonoBehaviour {

	private Text myText;
	private int yakuNum = 0;

	void Start(){
		this.myText = this.GetComponent<Text> ();
		this.Init ();
		this.Off ();
	}

	void Init(){
		this.SetText (yakuNum);
	}

	public void SetText(int yakuNum){
		this.yakuNum = yakuNum;

		this.myText.text = yakuNum.ToString(); 
	}

	public void Off(){
		this.myText.color = new Color (1.0f, 0.0f, 0.0f, 0.0f);
	}

	public void On(){
		this.myText.color = new Color (1.0f, 0.0f, 0.0f, 1.0f);
	}

}