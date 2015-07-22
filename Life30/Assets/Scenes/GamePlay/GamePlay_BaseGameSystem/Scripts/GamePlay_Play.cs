using UnityEngine;
using System.Collections;

public class GamePlay_Play : State {

public override void StateStart ()
{
	Debug.Log (this.ToString() + " Start!");

}

public override void StateUpdate ()
{

	if(Input.GetKeyDown(KeyCode.A)){
		this.EndState();
	}
	
}

public override void StateDestroy ()
{
	Debug.Log (this.ToString() + " Destroy!");
}

}
