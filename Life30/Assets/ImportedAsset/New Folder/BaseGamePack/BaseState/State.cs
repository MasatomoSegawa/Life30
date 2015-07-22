using UnityEngine;
using System.Collections;

public class State : MonoBehaviour {
	
	public StateEnum StateType;
	public StateEnum TransitionState;
	public _TransitionType TransitionType;
	protected ResourceManager ResourceManagerInstance;
	protected SoundManager SoundManagerInstance;

	public SoundManager SetSoundManagerInstance{
		set{ this.SoundManagerInstance = value; }
	}

	public ResourceManager SetResourceManagerInstance{
		set{this.ResourceManagerInstance = value;}
	}

	private bool isEnd = false;

	public delegate void End();
	public event End EndStateEvent;
		
	// Use this for initialization
	void Start () {
		StateStart();
	}

	void Awake(){
		AwakeStateStart ();
	}
	
	// Update is called once per frame
	void Update () {
		if(this.isEnd == false)
			StateUpdate();
	}
	
	void OnDestroy(){
		StateDestroy();
	}

	protected void EndState(){
		this.isEnd = true;
		EndStateEvent ();
	}

	public virtual void AwakeStateStart(){

	}

	public virtual void StateStart(){
		
	}
	
	public virtual void StateUpdate(){
		
	}
	
	public virtual void StateDestroy(){
		
	}

	public GameObject myInstantiate(GameObject obj){
		GameObject clone = Instantiate(obj)as GameObject;
		clone.name = obj.name;

		return clone;

	}
}
