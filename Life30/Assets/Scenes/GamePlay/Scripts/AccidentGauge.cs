using UnityEngine;
using System.Collections;

public class AccidentGauge : MonoBehaviour {

	public enum SindenzuState
	{
		Green,Yellow,Red,AccidentNow,
	}

	public delegate void doAccident (int a);
	public event doAccident AccidentEvent;

	public delegate void EndAccident (int a);
	public event EndAccident EndAccidentEvent;

	public Sprite[] Sprites;
	public SpriteRenderer Sindenzu;

	public int CurrentNumber = 0;

	private SindenzuState myState = SindenzuState.Green;

	public GameObject Effect;

	public GameObject SindenzuView;

	private float FinishTime = 0.0f;
	public float RateTime;
	private float CurrentTime = 0.0f;
	private bool isStartAccident = false;

	public GameObject AccidentCuttIn;
	public GameObject AccidentInfo;

	private GameObject tmp;
	private GameObject tmp2;

	//1番まで
	private int CurrentEventNumber;

	// Use this for initialization
	void Start () {

		AccidentEvent += (int a) => {
			Debug.Log("Accident!");
		};

		EndAccidentEvent += (int a) => {
			Debug.Log("End!");
		};

		myState = SindenzuState.Green;

		SetSindenzu (SindenzuState.Green);
	}

	void Update(){

		if (GameModel.isFreeze == false) {

			if (  Input.GetKeyDown (KeyCode.O)) {
				AccidentNow ();
			}

			if (myState == SindenzuState.AccidentNow) {
				if (isStartAccident == true) {
					isStartAccident = false;
					CurrentTime = Time.time;
					FinishTime += Time.time + RateTime;

				}

				CurrentTime += Time.deltaTime;

				if (CurrentTime >= FinishTime) {
					ResetAccidentGauge ();
				}

			}

		}

	}

	public void SetNumber(int num){
	
		if (myState == SindenzuState.AccidentNow)
			return;

		CurrentNumber += num;
		CurrentNumber = Mathf.Clamp (CurrentNumber, 0, 10);

		if (0 <= CurrentNumber && CurrentNumber <= 3) {
			myState = SindenzuState.Green;
		} else if (4 <= CurrentNumber && CurrentNumber <= 6) {
			myState = SindenzuState.Yellow;
		} else if (7 <= CurrentNumber && CurrentNumber <= 9) {
			myState = SindenzuState.Red;
		} else if (CurrentNumber == 10) {
			AccidentNow ();
		}

		SetSindenzu (myState);

	}

	//アクシデント開始時に呼ばれるメソッド
	void AccidentNow(){

		CurrentEventNumber = Random.Range (1, 6);

		Instantiate (AccidentCuttIn);
		tmp2 = Instantiate (AccidentInfo) as GameObject;
		tmp = Instantiate (Effect) as GameObject;

		tmp2.GetComponent<AccidentInfo> ().init (CurrentEventNumber);

		AccidentEvent (CurrentEventNumber);

		for(int i = 0; i < SindenzuView.transform.childCount; i++){
			GameObject view = SindenzuView.transform.GetChild (i).gameObject;
			if(view.GetComponent<SpriteRenderer>())
				view.GetComponent<SpriteRenderer> ().enabled = false;
		}

		myState = SindenzuState.AccidentNow;

		isStartAccident = true;

	}

	/// <summary>
	/// Accident終了時に呼ばれるメソッド
	/// </summary>
	public void ResetAccidentGauge(){

		FinishTime = 0.0f;
		CurrentNumber = 0;

		Destroy (tmp);
		Destroy (tmp2);

		for (int i = 0; i < SindenzuView.transform.childCount; i++) {
			GameObject view = SindenzuView.transform.GetChild (i).gameObject;
			if (view.GetComponent<SpriteRenderer> ())
				view.GetComponent<SpriteRenderer> ().enabled = true;
		}

		SetSindenzu (SindenzuState.Green);

		myState = SindenzuState.Green;

		EndAccidentEvent (CurrentEventNumber);
	}

	void SetSindenzu(SindenzuState state){
	
		switch (state) {
		case SindenzuState.Green:
			Sindenzu.sprite = Sprites [0];
			myState = SindenzuState.Green;
			break;

		case SindenzuState.Yellow:
			Sindenzu.sprite = Sprites [1];
			myState = SindenzuState.Yellow;
			break;

		case SindenzuState.Red:
			Sindenzu.sprite = Sprites [2];
			myState = SindenzuState.Red;
			break;
		}

	}

}
