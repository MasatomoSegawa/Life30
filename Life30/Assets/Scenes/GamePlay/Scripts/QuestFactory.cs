using UnityEngine;
using System.Collections;

//Lv	Next	Quest	Max	Min
struct QuestLevel
{
	public int Level;
	public int NextCount;
	public int QuestTime;
	public int Max, Min;
}

public class QuestFactory : MonoBehaviour
{
	public GameObject PrefabQuest;

	//Questのprefab
	public int _CurrentNumber;

	//現在のクリアに必要なクエストのヤク数
	public int ClearNum = 0;

	//Questを更新した数
	public int CurrentLevel = 1;

	[HideInInspector]
	public GameObject CurrentQuest;

	//現在のクエスト
	[HideInInspector]
	public Quest CurrentQuestScript;

	//現在のクエストのスクリプト
	//ゲーム調整用のcsvファイル
	private TextAsset GameLevel_csv;

	private QuestLevel CurrentQuestLevel;
	private QuestInfo CurrentQuestInfo;
	private string[] LevelStr;

	public delegate void DestroyQuest ();

	public event DestroyQuest DestroyQuestCallBack;

	public int CurrentNumber{ get { return _CurrentNumber; } }

	void Start ()
	{
		GameLevel_csv = (TextAsset)Instantiate (Resources.Load ("csv/LevelVer1"));

		if (GameLevel_csv == null) {
			Debug.LogError ("csvファイルないっす");
		}

		LevelStr = GameLevel_csv.text.Split ('\n');
		NextLevel (CurrentLevel);
	}

	//Questに必要な値を決めてクエストを生成する
	public void InstantQuest ()
	{
	
		if (ClearNum >= CurrentQuestLevel.NextCount) {
			CurrentLevel += 1;
			NextLevel (CurrentLevel);
		}

		_CurrentNumber = Random.Range (CurrentQuestLevel.Min, CurrentQuestLevel.Max);
		CurrentQuestInfo.YakuNum = _CurrentNumber;
		CurrentQuestInfo.QuestTime = CurrentQuestLevel.QuestTime;

		CurrentQuest = Instantiate (PrefabQuest)as GameObject;
		CurrentQuestScript = CurrentQuest.GetComponent<Quest> ();

		CurrentQuestScript.SetQuest (CurrentQuestInfo);
		CurrentQuestScript.LifeEndCallBack += EndQuestLifeTime;

		ClearNum++;
	}

	void EndQuestLifeTime ()
	{
		DestroyQuestCallBack ();
	}

	public void DestroyCurrentQuest ()
	{
		Destroy (CurrentQuest);
	}

	public void Freeze ()
	{
		Destroy (this.CurrentQuest);
	}

	void NextLevel (int level)
	{
		CurrentQuestLevel = InputParameter (level);
		ClearNum = 0;
	}

	//Lv	Next	Quest	Max	Min
	QuestLevel InputParameter (int num)
	{
		string[] str = LevelStr [num].Split (',');
		QuestLevel ql = new QuestLevel ();

		ql.Level = int.Parse (str [0]);
		ql.NextCount = int.Parse (str [1]);
		ql.QuestTime = int.Parse (str [2]);
		ql.Max = int.Parse (str [3]);
		ql.Min = int.Parse (str [4]);

		return ql;
	}

	int GetRandomNumber (int max, int min)
	{
		return Random.Range (min, max);
	}

	void DebugQuestInfo ()
	{
		Debug.Log ("QuestTime:" + CurrentQuestInfo.QuestTime + "YakuNum:" + CurrentQuestInfo.YakuNum);
	}
}
