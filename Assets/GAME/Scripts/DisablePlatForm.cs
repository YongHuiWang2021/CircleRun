using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisablePlatForm : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Ground") {
			other.gameObject.SetActive (false); //disable it..			
		}
	}
}
