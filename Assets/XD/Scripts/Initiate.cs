using UnityEngine;
using System.Collections;

public static class Initiate {
	public static void Begin (string scene,Color col,float damp){
		GameObject init = new GameObject ();
		init.name = "Begin";
		init.AddComponent<Fader> ();
		Fader scr = init.GetComponent<Fader> ();
		scr.fadeDamp = damp;
		scr.fadeScene = scene;
		scr.fadeColor = col;
		scr.start = true;
	}
	public static void ThenPlay ()
	{
		CircleMovement m = GameObject.FindObjectOfType<CircleMovement>();
		if (m)
		{
			m.StartTheGameE();
		}
	}
}
