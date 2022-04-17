using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]
public class AudioManager : MonoBehaviour {

	public static AudioManager _instance;

	//Sound..
	[HideInInspector]public AudioSource audioS;
	public AudioClip audioCoinCollect;
	public AudioClip audioWaterSplash;
	public AudioClip audioRecordBroke;
	public AudioClip audioGameOver;

	void Awake() {

		_instance = this;
	}

	// Use this for initialization
	void Start () {

		audioS = GetComponent<AudioSource> ();
	}
	
	public void PlayCoinCollect(){
		audioS.PlayOneShot (audioCoinCollect);
	}

	public void PlayWaterSplash(){
		audioS.PlayOneShot (audioWaterSplash);
	}

	public void PlayRecrod(){
		audioS.PlayOneShot (audioRecordBroke);
	}

	public void PlayGameOver(){
		audioS.PlayOneShot (audioGameOver);
	}

}
