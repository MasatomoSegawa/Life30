using UnityEngine;
using System.Collections;

public class particle : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (this.gameObject.GetComponent<ParticleSystem>().duration <= this.gameObject.GetComponent<ParticleSystem>().time) {
			Destroy(this.gameObject);
		}

	}
}
