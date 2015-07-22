using UnityEngine;
using System.Collections;

public class GameEnd_Initialize : State {

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
