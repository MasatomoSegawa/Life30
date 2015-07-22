using UnityEngine;
using System.Collections;

public class ButtonController : MonoBehaviour
{
	private int pointer = 0;
	private Animator[] anims;
	public GameObject[] Objs;
	private bool Freeze = false;

	public delegate void InputStartButton ();

	public event InputStartButton InputStartButtonEvent;

	public delegate void InputRankingButton ();

	public event InputRankingButton InputRankingButtonEvent;

	// Use this for initialization
	void Start ()
	{
		anims = new Animator[2];
		anims [0] = Objs [0].GetComponent<Animator> ();
		anims [1] = Objs [1].GetComponent<Animator> ();
	}

	public void OnFreeze ()
	{
		Freeze = true;
	}

	public void OnRetry ()
	{
		Freeze = false;
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Q)) {
			Application.Quit ();
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			PlayerPrefs.DeleteAll ();
		}

		if (Freeze != true) {

			if (Input.GetAxisRaw ("Vertical") > 0 && pointer == 1) {
				anims [pointer].SetBool ("isFocus", false);
				pointer = (pointer + 1) % 2;
				SoundManager.Instance.PlaySE ("Select");
			}
			if (Input.GetAxisRaw ("Vertical") < 0 && pointer == 0) {
				anims [pointer].SetBool ("isFocus", false);
				pointer = (pointer + (2 - 1)) % 2;
				SoundManager.Instance.PlaySE ("Select");
			}

			//init
			foreach (GameObject obj in Objs) {
				obj.GetComponent<TextMesh> ().color = Color.black;
			}

			anims [pointer].SetBool ("isFocus", true);
			Objs [pointer].GetComponent<TextMesh> ().color = Color.red;

			if (Input.GetKeyDown (KeyCode.Z)) {
				switch (pointer) {
				case 0:
					InputStartButtonEvent ();
					break;

				case 1:
					InputRankingButtonEvent ();
					break;
				}
				SoundManager.Instance.PlaySE ("Decide");
				this.OnFreeze ();
			}

		}
	}
}

