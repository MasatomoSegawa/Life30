using UnityEngine;
using System.Collections;

public class Initailizer : State
{

	public GameObject Text;

	private GameObject Player;
	private LifeTimer lifeTimer;

	public override void StateStart ()
	{
		Text = Instantiate(Text)as GameObject;

		Player = GameObject.FindGameObjectWithTag("Player");
		Player.SendMessage("Freeze",SendMessageOptions.DontRequireReceiver);
		GameModel.Instance.SetFreeze ();

		lifeTimer = GameObject.FindGameObjectWithTag("LifeTimer").GetComponent<LifeTimer>();
	}

	public override void StateUpdate ()
	{

		if(Input.anyKeyDown){
			this.EndState ();
		}

	}

	public override void StateDestroy ()
	{

		GameModel.Instance.SetRetry ();
		lifeTimer.isStop = false;
		if(Player != null)
			Player.SendMessage("ResetPlayer",SendMessageOptions.DontRequireReceiver);
		Destroy (Text);
	}
}
