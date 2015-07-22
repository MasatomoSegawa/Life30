using UnityEngine;
using System.Collections;

public enum TransitionEffectType
{
	FadeIn,
	FadeOut,
};

public class Effects : MonoBehaviour
{
	public delegate void EndFadeOut ();

	public delegate void EndFadeIn ();

	public event EndFadeOut EndFadeOutEvent;
	public event EndFadeIn EndFadeInEvent;

	private Animator anim;

	// Use this for initialization
	void Awake ()
	{
		this.anim = this.GetComponent<Animator> ();
	}

	public void SetTransition (TransitionEffectType TE)
	{

		switch (TE) {

		case TransitionEffectType.FadeIn:
			this.anim.SetTrigger ("doFadeIn");
			break;

		case TransitionEffectType.FadeOut:
			
			this.anim.SetTrigger ("doFadeOut");
			break;

		}

	}

	void OnEndFadeIn ()
	{
		this.anim.SetTrigger ("EndEffect");
		EndFadeInEvent ();
	}

	void OnEndFadeOut ()
	{
		this.anim.SetTrigger ("EndEffect");
		EndFadeOutEvent ();			
	}

}
