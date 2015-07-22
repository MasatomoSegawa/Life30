using UnityEngine;
using System.Collections;

public class AccidentEffectManager : MonoBehaviour
{
	public GameObject Effect;
	public GameObject tmp;

	public void SetEffect(){
		tmp = Instantiate (Effect)as GameObject;
	}

	public void EndEffect(){
		Destroy (tmp);
	}

}
