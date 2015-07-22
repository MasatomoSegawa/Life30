using UnityEngine;
using System.Collections;

public class RankingIdle : State
{

	private bool isFreeze = false;
	private float NextTime;
	private float RateTime = 1.0f;

	public override void StateStart ()
	{
		Debug.Log (this.ToString () + " Start!");

		SoundManager.Instance.PlayBGM ("Ranking");

		NextTime = Time.time + RateTime;
	}

	public override void StateUpdate ()
	{

		if (Input.GetKeyDown (KeyCode.Z) && NextTime <= Time.time && isFreeze == false) {
			FadeManager.Instance.LoadLevel ("Title", 1.0f, _FadeColor.Black);
			isFreeze = true;
		}

	}

	public override void StateDestroy ()
	{
		Debug.Log (this.ToString () + " Destroy!");
	}
}
