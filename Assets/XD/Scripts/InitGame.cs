#define XDPUBLIC
using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;
using XD.Scripts;
using XD.Scripts.RV;

public class InitGame : MonoBehaviour
{

	// Use this for initialization
	void Start () {
		MainGame.Instance.InitData(() => { });
	}
	
}
