using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

	public float   rotSpeed = 1f;
	public Vector3 direction = new Vector3 (0, 0, 1f);

	void Update()
	{
		// Rotate the object around its local X axis at 1 degree per second
		transform.Rotate(direction * Time.deltaTime * rotSpeed);

	}
}
