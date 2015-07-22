using UnityEngine;
using System.Collections;

public class GameModel : Singleton<GameModel> {

	public static bool isFreeze;

	public void SetFreeze(){
		isFreeze = true;
	}

	public void SetRetry(){
		isFreeze = false;
	}

}
