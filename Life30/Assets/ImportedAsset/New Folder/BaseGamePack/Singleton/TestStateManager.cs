using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// StateManager更新版
/// </summary>
public class TestStateManager : MonoBehaviour {

	private ResourceManager resourceManager;
	private TransitionManager transitionManager;
	private SoundManager soundManager;

	private GameObject currentTask;
	private State currentScript;
	private GameObject NextTask;

	//複数の遷移専用のGameObject
	public GameObject[] States;

	Dictionary<StateEnum,GameObject> StateTransitionTable;

	// Use this for initialization
	void Awake ()
	{
		this.resourceManager = this.GetComponent<ResourceManager> ();
		this.transitionManager = this.GetComponent<TransitionManager> ();
		this.soundManager = this.GetComponent<SoundManager> ();

		this.StateTransitionTable = new Dictionary<StateEnum, GameObject> ();

		if (States.Length == 0)
			Debug.LogError ("ルートステート登録されてないっす");

		//初期化状態を生成.
		currentTask = myInstantiate (States [0]);

		foreach (GameObject obj in States) {
			State _state = obj.GetComponent<State> ();
			StateTransitionTable.Add (_state.StateType,obj);
		}

	}

	GameObject myInstantiate (GameObject obj)
	{
		if (obj == null) {
			Debug.LogError (currentScript.TransitionState.ToString () + " is None!");
			Application.Quit ();
			return null;
		}

		GameObject clone = Instantiate (obj)as GameObject;
		currentScript = clone.GetComponent<State> ();

		currentScript.SetResourceManagerInstance = this.resourceManager;
		currentScript.SetSoundManagerInstance = this.soundManager;
		currentScript.EndStateEvent += EndState;

		clone.name = obj.name;

		return clone;
	}

	void EndState ()
	{
		this.transitionManager.TransitionState (SwitchState, DestroyCurretnState, currentScript.TransitionType);
	}

	void DestroyCurretnState(){
	
		//前の状態を終了処理させる.
		if (currentTask != null && currentScript.TransitionState != StateEnum.e_NONE) {
			Destroy (currentTask);
		}
	}

	void SwitchState(){

		if (currentScript.TransitionState != StateEnum.e_NONE)
			currentTask = myInstantiate (StateTransitionTable [currentScript.TransitionState]);


	}

}
