//#define OpenRVTest
using System;
using System.Collections;
using System.Text;
using Scripts.Common.Factory;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using XD.Scripts.RV;
using XD.Scripts.RV.ADS;
using XD.XDLog;

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
#if OpenRVTest
		ShowGUI = true;
#endif
		
		XDDebug.Init();
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



	
	
	IEnumerator StartGame()
	{
		GameRVManager.Instance.Init();
		
		yield return new WaitForSeconds(4);
		if (ShowGUI)
		{
		//	GameRVManager.Instance.ShowRV();
		}
		else
		{
			SceneManager.LoadScene("wallBall");
			//Initiate.Begin("Game", SystemParam.Instance.GameColor, SystemParam.Instance.Damp);
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
		GUILayout.Label("init OK ? " + GameRVManager.Instance.RVIsReady,usingColor);

		if (GUILayout.Button("Play", GUILayout.Width(100), GUILayout.Height(100)))
		{
			GameRVManager.Instance.ShowRV();
		}
		if (GUILayout.Button("EXT", GUILayout.Width(100), GUILayout.Height(100)))
		{
		
			Application.Quit();
		}
		GUILayout.Label(XDDebug.LogData.ToString());
	}

	
	
	
    }
}