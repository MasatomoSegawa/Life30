using UnityEngine;
using System.Collections;

public class AccidentEffect : MonoBehaviour {

	public float speed;

	private Material _Material;

	// Use this for initialization
	void Start () {
		_Material = this.gameObject.GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
	

		_Material.mainTextureOffset = new Vector2 (Time.time * speed, 0);

	}

}
