using UnityEngine;
using System;
using UnityEngine.UI;

public class LifeTimer : MonoBehaviour
{

	private Text myTextMesh;
	private float _NowLifeTime;
	private bool _isEnd = false;

	public GameObject LifeUpEffect;
	public GameObject LifeDownEffect;
	public Animator ShinikakeEffect;

	public float InitLifeTime = 30.0f;
	public float SumLifeTime = 0.0f;
	public bool isStop = true;

	public float NowLifeTime{ get { return _NowLifeTime; } }
	public bool isEnd{ get { return _isEnd; } }

	// Use this for initialization
	void Awake ()
	{
		this.myTextMesh = this.GetComponent<Text> ();
		if (this.myTextMesh) {
			this.InitTextMesh ();
		}
	}

	void Update ()
	{
		if (GameModel.isFreeze == false) {
			if (isStop == false && _isEnd == false) {
				this.Clock ();
			}

		}
	}

	void EndTimer ()
	{
		this._isEnd = true;
		this.isStop = true;
		this._NowLifeTime = 0.0f;
		ReWriteText ();
	}

	void Clock ()
	{
		this._NowLifeTime -= Time.deltaTime;
		SumLifeTime += Time.deltaTime;

		if (this._NowLifeTime >= 0.0f) {
			ReWriteText();
		} else
			this.EndTimer ();
			
	}

	void ReWriteText(){
		if (this._NowLifeTime < 10) {
			this.myTextMesh.text = "余命:<color=red>" + _NowLifeTime.ToString ("F2") + "</color>";
			//this.ShinikakeEffect.SetBool ("On", true);
		} else {
			this.myTextMesh.text = "余命:" + _NowLifeTime.ToString ("F2");
			//this.ShinikakeEffect.SetBool ("On", false);
		}
	}

	//寿命（タイマー）を増やすメソッド,マイナスの値も来るよ
	public void AddTime(int num){

		this._NowLifeTime += num;
		GameObject clone;
		if(num > 0)
			clone = Instantiate(LifeUpEffect)as GameObject;
		else
			clone = Instantiate(LifeDownEffect)as GameObject;

		clone.GetComponent<FadeOut>().SetUp(num);

		ReWriteText();
	}

	public void InitTextMesh ()
	{
		this._NowLifeTime = this.InitLifeTime;
		this.myTextMesh.text = "余命:" + _NowLifeTime.ToString ("F2");
		this.isStop = true;
	}

	public void StartLifeTimer ()
	{

	}
	

}
