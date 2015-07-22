using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public struct QuestInfo{
	public float QuestTime;
	public int YakuNum;
}

public class Quest : MonoBehaviour {

	private QuestInfo questInfo;
	private const string str = "粒飲め！";
	private TextMesh myTextMesh;
	private Color TransParentColor;
	private float cnt;

	public float time;

	public delegate void LifeEndEvent();
	public event LifeEndEvent LifeEndCallBack;

	void Awake(){
		this.myTextMesh = this.GetComponent<TextMesh>();

		TransParentColor = this.myTextMesh.color;
		TransParentColor.a = 0.0f;
	}

	public void SetQuest(QuestInfo info){
		this.questInfo = info;
		this.myTextMesh.text = questInfo.YakuNum + str;
	}

	// Update is called once per frame
	void Update () {

		if (GameModel.isFreeze == false) {
	
			time = questInfo.QuestTime;

			cnt += Time.deltaTime;
			myTextMesh.color = Color.Lerp (myTextMesh.color, TransParentColor, cnt / (questInfo.QuestTime * (60.0f + 5.5f)));

			questInfo.QuestTime -= Time.deltaTime;
			if (questInfo.QuestTime <= 0.0f) {
				LifeEndCallBack ();
			}
		}

	}

}
