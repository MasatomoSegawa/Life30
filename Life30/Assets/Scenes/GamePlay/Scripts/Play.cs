using UnityEngine;
using System.Collections;

public class Play : State
{

	private LifeTimer lifeTimer;
	private SiasakiChanController Siasaki;
	private QuestFactory questFactory;
	private Yakubin yakubin;
	private GameObject HealingEffect;

	private GameObject cloneDamageEffect;

	public float RateQuestTime = 0.5f;

	public override void StateStart ()
	{
		lifeTimer = GameObject.FindGameObjectWithTag("LifeTimer").GetComponent<LifeTimer>();
		Siasaki = GameObject.FindGameObjectWithTag("Player").GetComponent<SiasakiChanController>();
		questFactory = GameObject.FindGameObjectWithTag("QuestFactory").GetComponent<QuestFactory>();
		yakubin = this.Siasaki.yakubin.GetComponent<Yakubin>();
		HealingEffect = this.ResourceManagerInstance.GetPrefab ("Eff_Heal_1");

		questFactory.InstantQuest();
		lifeTimer.isStop = false;

		//コールバック登録
		Siasaki.NomikomuCallback += NomikomiEvent;
		Siasaki.Nomikomu_to_Idle_Callback += Nomikomu_to_Idle_Event;
		Siasaki.CreateYakuCallback += CreateYakuEvent;
		Siasaki.Damage_to_Idle_Callback += Damage_to_Idle_Event;
		questFactory.DestroyQuestCallBack += QuestTimeUpEvent;

		SoundManager.Instance.PlayBGM ("PlayBGM");
	}

	public override void StateUpdate ()
	{

		//終了条件
		if (lifeTimer.isEnd == true) {
			this.EndState ();
		}

		if (Input.GetKeyDown (KeyCode.Q)) {
			Application.Quit ();
		}


	}

	public override void StateDestroy ()
	{

		if(questFactory != null){
			questFactory.Freeze();
		}
		
	}

	/*
	//アクシデントゲージがMAX時に呼ばれるイベント
	void AccidentGaugeEvent(int number){

		Debug.Log ("Accident " + number + " Start!");

		switch (number) {
		case 1:
			Siasaki.CreateYakuCallback -= CreateYakuEvent;
			Siasaki.CreateYakuCallback += OnAccident1;
			break;

		case 2:
			Siasaki.CreateYakuCallback -= CreateYakuEvent;
			Siasaki.CreateYakuCallback += OnAccident2;
			break;

		case 3:
			Siasaki.NomikomuCallback -= NomikomiEvent;
			Siasaki.NomikomuCallback += OnAccident3;
			break;

		case 5:
			OnAccident5 ();
			break;
		}

	}

	//アクシデント終了時に呼ばれるイベント
	void AccidentEndEvent(int number){

		Debug.Log ("Accident " + number + " End!");

		switch (number) {
		case 1:
			Siasaki.CreateYakuCallback -= OnAccident1;
			Siasaki.CreateYakuCallback += CreateYakuEvent;
			break;

		case 2:
			Siasaki.CreateYakuCallback -= OnAccident2;
			Siasaki.CreateYakuCallback += CreateYakuEvent;
			break;

		case 3:
			Siasaki.NomikomuCallback -= OnAccident3;
			Siasaki.NomikomuCallback += NomikomiEvent;
			break;

		case 5:
			EndAccident5 ();
			break;
		

		}

	}

	/// <summary>
	/// アクシデント番号１番
	/// 超常現象発生！ゆっくり振るほどたくさん出る！
	/// ゆっくり降るほど薬が出てくる
	/// </summary>
	/// <param name="AgeSageTime">Age sage time.</param>
	void OnAccident1(float AgeSageTime){
	
		if (AgeSageTime >= 1.0f) {
			yakubin.CreateYaku (4);
		} else if (0.6f < AgeSageTime && AgeSageTime <= 0.9f) {
			yakubin.CreateYaku (3);
		} else if (0.3f < AgeSageTime && AgeSageTime <= 0.6f) {
			yakubin.CreateYaku (2);
		}
		if (0.3f > AgeSageTime) {
			yakubin.CreateYaku (1);
		}

	}

	/// <summary>
	/// アクシデント番号２番
	/// 動機が激しくなってきた！ビンを振る手が落ち着かない！
	/// </summary>
	/// <param name="AgeSageTime">Age sage time.</param>
	void OnAccident2(float AgeSageTime){

		if (AgeSageTime >= 1.0f) {
			yakubin.CreateYaku (2);
		} else if (0.6f < AgeSageTime && AgeSageTime <= 0.9f) {
			yakubin.CreateYaku (4);
		} else if (0.3f < AgeSageTime && AgeSageTime <= 0.6f) {
			yakubin.CreateYaku (6);
		}
		if (0.3f > AgeSageTime) {
			yakubin.CreateYaku (8);
		}

	}

	/// <summary>
	/// アクシデント番号３番
	/// 中毒が悪化！薬の副作用が強くなる！
	/// ぴったし分以外の寿命上昇が半減
	/// マイナスが-１秒固定から-(誤差粒の数)秒になる
	/// </summary>
	void OnAccident3(){

		int YakuNum = this.yakubin.DeleteYaku ();
		int QuestNum = this.questFactory.CurrentNumber;
		int ans = CalculateAddLifeTime (Mathf.Abs (YakuNum - QuestNum));

		if (ans < 0) {
			ans = -Mathf.Abs (YakuNum - QuestNum);
		} else {
			ans /= 2;
		}

		if(YakuNum == 0){
			this.Siasaki.PlayerState = YakukoState.Idle;
			return;
		}

		this.lifeTimer.GetComponent<LifeTimer> ().AddTime (ans);
		this.questFactory.DestroyCurrentQuest();

	}

	/// <summary>
	///アクシデント番号４番
	///頭がいたい！薬を飲まないとフラフラする！
	///クエスト制限時間に間に合わなかった時の硬直時間が倍になる
	/// </summary>
	void OnAccident4(){

	}

	/// <summary>
	///目が霞んできた！手のひらに出している薬がよく見えない！！
	///手のひらに出している薬の総量が見えなくなる
	/// </summary>
	void OnAccident5(){
	
		GameObject bigHandCamera = GameObject.FindGameObjectWithTag ("BigHandCamera");
		bigHandCamera.GetComponent<Animator> ().SetBool ("isAccident", true);

	}

	/// <summary>
	/// Accident5終了時に呼ばれるメソッド
	/// </summary>
	void EndAccident5(){

		GameObject bigHandCamera = GameObject.FindGameObjectWithTag ("BigHandCamera");
		bigHandCamera.GetComponent<Animator> ().SetBool ("isAccident", false);
	}
	*/

	//Questの寿命が切れた時に呼ばれるイベント
	void QuestTimeUpEvent(){

		Destroy(this.questFactory.CurrentQuest);
		this.Siasaki.GetComponent<Animator>().SetTrigger("Damage");
		SoundManager.Instance.PlaySE ("Damage");
		this.yakubin.DeleteYaku();
		this.lifeTimer.GetComponent<LifeTimer> ().AddTime (-5);

	}

	void Damage_to_Idle_Event(){
	
		this.questFactory.InstantQuest();
		if(cloneDamageEffect != null){
			Destroy(cloneDamageEffect);
		}

	}

	//ヤクコさんが薬を飲み込んだ時の処理
	void NomikomiEvent(){

		int YakuNum = this.yakubin.DeleteYaku ();
		int QuestNum = this.questFactory.CurrentNumber;
		int ans = CalculateAddLifeTime (Mathf.Abs (YakuNum - QuestNum));
		
		if(YakuNum == 0){
			this.Siasaki.PlayerState = YakukoState.Idle;
			return;
		}

		if (ans < 0) {
			SoundManager.Instance.PlaySE ("Damage");
			this.Siasaki.GetComponent<Animator> ().SetTrigger ("Damage");
			ans = -5;
		} else {
			SoundManager.Instance.PlaySE ("Heal");
		}


		this.lifeTimer.GetComponent<LifeTimer> ().AddTime (ans);
		this.questFactory.DestroyCurrentQuest();

	}

	//ヤクコさんが薬を飲み込んだ後アイドル状態になった時の処理
	void Nomikomu_to_Idle_Event(){
		questFactory.InstantQuest();
	}

	//ヤクコさんが薬ビンを振った時の処理
	void CreateYakuEvent(float AgeSageTime){

		int num = myTimer.Float2YakuNum (AgeSageTime);

		yakubin.CreateYaku (num);

	}

	//飲んだ個数によって寿命を増やすか減らすか決めるメソッド
	int CalculateAddLifeTime (int ans)
	{

		int num = 0;
		
		switch (ans) {
		case 0:
			num = 5;
			Instantiate (HealingEffect);
			break;
			
		case 1:
			num = 3;
			Instantiate (HealingEffect);
			break;
			
		case 2:
			num = 2;
			Instantiate (HealingEffect);
			break;
			
		case 3:
			num = 1;
			Instantiate (HealingEffect);
			break;
			
		default:
			num = -1;
			break;
		}
		
		return num;
	}

}