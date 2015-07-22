using UnityEngine;
using System.Collections;

public sealed class myTimer
{
	private float _NowTime = 0.0f;
	private bool _isWork = false;

	public bool isWork{ get { return _isWork; } }

	public int CurrentYakuNum {
		get {
			return myTimer.Float2YakuNum (this._NowTime);
		}
	}

	public void Stop ()
	{
		_isWork = false;
	}

	public void Start ()
	{
		_isWork = true;
	}

	public void Reset ()
	{
		_isWork = false;
		_NowTime = 0.0f;
	}

	public float GetAgeSageTime ()
	{
		return _NowTime;
	}

	public void Update (float currentTime)
	{

		if (this._isWork == true) {
			_NowTime += currentTime;
		}

	}

	/**
	 * 秒数から薬品の数を変換.
	 **/
	public static int Float2YakuNum (float AgeSageTime)
	{

		if (AgeSageTime >= 1.0f) {
			return 1;
		} else if (0.6f < AgeSageTime && AgeSageTime < 1.0f) {
			return 2;
		} else if (0.3f < AgeSageTime && AgeSageTime <= 0.6f) {
			return 3;
		}
		if (0.3f >= AgeSageTime) {
			return 4;
		}

		return 0;
	}
}

public enum YakukoState
{
	Idle,
	Ageru,
	AgeruIdle,
	AgeruNomu,
	Sageru,
	Nomikomu,
	Damage,
};

public class SiasakiChanController : MonoBehaviour
{
	private Animator animator;
	private Script_SpriteStudio_PartsRoot YakukoController;
	// 上げてから下げての時間を計測するタイマー
	private myTimer timer;
	// 薬品の数を予告するGUI
	public PreYakuNumText PreYakuNumText;
	// 上げてから下げての時間
	private float AgeSageTime = 0.0f;
	public YakukoState PlayerState;
	public GameObject Yakuko;
	public GameObject yakubin;
	public GameObject BigHand;
	public GameObject LifeTimer;

	public delegate void NomikomuCallBack ();

	public event NomikomuCallBack NomikomuCallback;

	public delegate void Nomikomu_to_Idle_CallBack ();

	public event Nomikomu_to_Idle_CallBack Nomikomu_to_Idle_Callback;

	public delegate void CreateYakuCallBack (float AgeSageTime);

	public event CreateYakuCallBack CreateYakuCallback;

	public delegate void DamageCallBack ();

	public event DamageCallBack Damage_to_Idle_Callback;
	// Use this for initialization
	void Awake ()
	{
		this.animator = this.GetComponent<Animator> ();
		this.YakukoController = Yakuko.GetComponent<Script_SpriteStudio_PartsRoot> ();

		this.YakukoController.AnimationPlay (4, 0, 1, 1.0f);

		this.PlayerState = YakukoState.Sageru;

		this.timer = new myTimer ();
	}

	// Update is called once per frame
	void Update ()
	{
		Debug.Log (Input.touches.Length);
		if (GameModel.isFreeze == false && Input.touches.Length > 0) {
		
			Debug.Log ("test");

			Touch touch = Input.GetTouch (0);

			switch (touch.phase) {
			case TouchPhase.Began:
				animator.SetBool ("InputZ", true);
				break;

			case TouchPhase.Canceled:
			case TouchPhase.Ended:

				break;

			case TouchPhase.Stationary:
				break;
			}

		}

		#region キー入力
		bool InputZ = false, InputX = false, InputC = false;

		if (GameModel.isFreeze == false) {

			InputZ = Input.GetKeyDown (KeyCode.Z);
			InputX = Input.GetKeyDown (KeyCode.X);
			InputC = Input.GetKeyDown (KeyCode.C);

			animator.SetBool ("InputZ", InputZ);
			animator.SetBool ("InputX", InputX);
			animator.SetBool ("InputC", InputC);

			if (this.yakubin.GetComponent<Yakubin> ().currentYakuNum > 0) {
				animator.SetBool ("hasKusuri", true);
			} else {
				animator.SetBool ("hasKusuri", false);
			}

			timer.Update (Time.deltaTime);

			// タイマーが動いてる時に薬品の数を予告するGUIを更新する.
			if (timer.isWork == true) {
				int num = timer.CurrentYakuNum;
				this.PreYakuNumText.SetText (num);
			}
		}
		#endregion
	}
	//
	//ここから下はAnimation Event関数に登録された関数群
	//
	void Idle ()
	{

		switch (PlayerState) {
		case YakukoState.AgeruNomu:
		case YakukoState.Nomikomu:
			Nomikomu_to_Idle_Callback ();
			break;

		case YakukoState.Damage:
			Damage_to_Idle_Callback ();
			break;
		}

		this.YakukoController.AnimationPlay (6, 0, 1, 1.0f);
		this.PlayerState = YakukoState.Idle;
		BigHand.SetActive (true);

	}

	void Damage ()
	{
		this.YakukoController.AnimationPlay (5, 0, 1, 1.0f);
		this.PlayerState = YakukoState.Damage;

		this.timer.Reset ();
		PreYakuNumText.Off ();

		BigHand.SetActive (false);
	}

	void AgeruNomu ()
	{
		this.YakukoController.AnimationPlay (2, 0, 1, 1.0f);
		this.PlayerState = YakukoState.AgeruNomu;
		BigHand.SetActive (false);
		PreYakuNumText.Off ();

		NomikomuCallback ();
	}

	void AgeruIdle ()
	{
		this.YakukoController.AnimationPlay (1, 0, 1, 1.0f);
		this.PlayerState = YakukoState.AgeruIdle;
		timer.Start ();
		PreYakuNumText.On ();

		SoundManager.Instance.PlaySE ("HuriAge");
	}

	void Ageru ()
	{
		this.YakukoController.AnimationPlay (0, 1, 1, 1.0f);
		this.PlayerState = YakukoState.Ageru;
		timer.Start ();
		PreYakuNumText.Off ();

		//SoundManager.Instance.PlaySE ("HuriAge");
	
	}

	void Sageru ()
	{
		this.YakukoController.AnimationPlay (8, 1, 1, 1.0f);
		this.PlayerState = YakukoState.Sageru;
		AgeSageTime = timer.GetAgeSageTime ();
		timer.Reset ();
		PreYakuNumText.Off ();

		CreateYakuCallback (AgeSageTime);
		SoundManager.Instance.PlaySE ("HuriSage");
	}

	void Nomikomu ()
	{
		this.YakukoController.AnimationPlay (7, 1, 1, 1.0f);
		this.PlayerState = YakukoState.Nomikomu;
		BigHand.SetActive (false);
		PreYakuNumText.Off ();
		timer.Reset ();

		NomikomuCallback ();
	}
}
