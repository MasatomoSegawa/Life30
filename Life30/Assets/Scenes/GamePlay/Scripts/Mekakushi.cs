using UnityEngine;
using System.Collections;

public class Mekakushi : MonoBehaviour
{
	private float pointA = -16.4f;
	private float pointB = -7.65f;

	public float speed = 1.0f;

	// Use this for initialization
	void Start ()
	{
		Init ();
	}

	void Init ()
	{

		Vector3 tmp = this.transform.position;
		tmp.x = pointA;

		this.transform.position = tmp;

	}

	// Update is called once per frame
	void Update ()
	{
	
		Vector3 tmp = this.transform.position;
		tmp.x = Mathf.Clamp (this.transform.position.x + Time.deltaTime * speed, pointA, pointB);

		this.transform.position = tmp;

		if (this.transform.position.x >= pointB)
			Init ();
	}
}
