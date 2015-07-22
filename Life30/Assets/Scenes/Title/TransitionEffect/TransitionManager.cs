using UnityEngine;
using System.Collections;

public enum _TransitionType{
	No_Effect,FadeOut_In,
}

public class TransitionManager : MonoBehaviour
{
	//FadeOut/FadeIn
	public delegate void OnEndFadeOut ();
	public delegate void OnEndFadeIn ();

	public GameObject EffectObj;

	[HideInInspector]
	public TransitionEffectType CurrentEffectType;

	private GameObject CurrentEffectObj;
	private Effects CurrentEffectScript;

	public void FadeOut_FadeIn_Transition(OnEndFadeIn FadeIn,OnEndFadeOut Fadeout){

		CurrentEffectObj = Instantiate (EffectObj)as GameObject;
		CurrentEffectScript = CurrentEffectObj.GetComponentInChildren<Effects> ();
		CurrentEffectScript.EndFadeOutEvent += () => {
			Fadeout ();
			doFadeIn ();
		};

		CurrentEffectScript.EndFadeInEvent += () => {
			FadeIn ();
			Destroy (CurrentEffectObj);
		};

		doFadeOut ();
	}

	public void TransitionState(OnEndFadeIn FadeIn,OnEndFadeOut FadeOut,_TransitionType TT){

		switch (TT) {
		case _TransitionType.FadeOut_In:
			CurrentEffectObj = Instantiate (EffectObj)as GameObject;
			CurrentEffectScript = CurrentEffectObj.GetComponentInChildren<Effects> ();
			CurrentEffectScript.EndFadeOutEvent += () => {
				FadeOut();
				doFadeIn ();
			};

			CurrentEffectScript.EndFadeInEvent += () => {
				FadeIn ();
				Destroy (CurrentEffectObj);
			};

			doFadeOut ();
			break;

		case _TransitionType.No_Effect:
			FadeOut ();
			FadeIn ();
			break;
		}

	}
		
	void doFadeOut ()
	{
		CurrentEffectType = TransitionEffectType.FadeOut;
		doEffect ();
	}

	void doFadeIn ()
	{
		CurrentEffectType = TransitionEffectType.FadeIn;
		doEffect ();
	}

	void doEffect ()
	{
		this.CurrentEffectScript.SetTransition (CurrentEffectType);
	}
}
