#define OpenRVTest
using System;
using System.Collections;
using Scripts.Common.Factory;
using UnityEngine;
using UnityEngine.Advertisements;
using XD.Scripts.RV;
using XD.Scripts.RV.ADS;

namespace XD.Scripts
{
    public class MainGame : SingletonMonoBehaviour<MainGame>
    {
#if OpenRVTest
private bool ShowGUI = true;
#else	 
	    private bool ShowGUI = false;
#endif    
	
	
	// Use this for initialization
	public override void InitData(Action InitOK)
	{

	}

	private void Awake()
	{
#if XDPUBLIC
		ShowGUI = true;
#endif
		string logName = DateTime.Now.ToString();
		LoggerWriter.Instance.InitWriter(logName);
		StartCoroutine(Waite());
		Screen.orientation = ScreenOrientation.Portrait;
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		m_tempFontStyle.normal.textColor = Color.red;
		m_tempFontStyle2.normal.textColor = Color.green;
		m_tempFontStyle.fontSize = 20;
		m_tempFontStyle2.fontSize = 20;
		m_tempFontStyle3.normal.textColor = Color.white;
		m_tempFontStyle3.fontSize = 20;
		StartCoroutine(StartGame());
	}

	private WaitForEndOfFrame mWaitForEndOfFrame = new WaitForEndOfFrame();
	IEnumerator Waite()
	{
		while (true)
		{
			yield return mWaitForEndOfFrame;
			LoggerWriter.Instance.SaveLog();
		}
	}
	IEnumerator StartGame()
	{
		GameRVManager.Instance.Init();
		string logName = DateTime.Now.ToString();
		LoggerWriter.Instance.InitWriter(logName);
		
		yield return new WaitForSeconds(4);
		if (ShowGUI)
		{
		//	GameRVManager.Instance.ShowRV();
		}
		else
		{
			Initiate.Begin("Game", SystemParam.Instance.GameColor, SystemParam.Instance.Damp);
		}
    }
	private GUIStyle m_tempFontStyle = new GUIStyle();
	private GUIStyle m_tempFontStyle2 = new GUIStyle();
	private GUIStyle m_tempFontStyle3 = new GUIStyle();
	private void OnGUI()
	{
		if(!ShowGUI)
			
			return;


		GUIStyle usingColor = GameRVManager.Instance.RVIsReady ? m_tempFontStyle2 : m_tempFontStyle;
		GUILayout.Label(" ：：：" + SystemInfo.operatingSystem,m_tempFontStyle3);
	//	GUILayout.Label("GoogleADS init OK ? " + GoogleADS.Instance.InitOK);

		if (GUILayout.Button("Play", GUILayout.Width(100), GUILayout.Height(100)))
		{
			GameRVManager.Instance.ShowRV();
		}
	}

	public override void OnDestroy()
	{
		LoggerWriter.Instance.Cancellation();
	}
    }
}