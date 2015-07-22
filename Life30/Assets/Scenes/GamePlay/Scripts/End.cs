using UnityEngine;
using System.Collections;

public class End : State
{

	public override void StateStart ()
	{
		LifeTimer lifeTimer = GameObject.FindGameObjectWithTag ("LifeTimer").GetComponent<LifeTimer> ();

		PlayerPrefs.SetInt ("CurrentScore", (int)lifeTimer.SumLifeTime);
		PlayerPrefs.Save ();

		SoundManager.Instance.PlaySE ("End");
	}

	public override void StateUpdate ()
	{

		GameModel.isFreeze = true;
		FadeManager.Instance.LoadLevel ("GameEnd", 1.2f,_FadeColor.Red);

	}


	public override void StateDestroy ()
	{

				
	}

}
