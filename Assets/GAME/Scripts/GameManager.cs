using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class GameManager : MonoBehaviour {

	public static GameManager _instance { get; set; }


	[Header("GAME-UI")]
	public GameObject  panelDetail;
	public GameObject  canvasTutorial;
	public Text		   scoreUi;
	public Text		   topScoreUi;

	[Header("Stages")]
	public  GameObject[]    	m_stagePrefabs;
	public  Transform  			m_stageParentGroup;
	public  Transform 			m_SpanwerTrigger;
	private List<GameObject> 	m_stageSpawnPool     = new List<GameObject>();
	private List<GameObject> 	m_stageSpawnPoolUsed = new List<GameObject>();
	private int        			m_stagePrefabsPrevSpawnedIndex = 0;

	[Header("BACKGROUND")]
	public SpriteRenderer bgRenderer;
	public Sprite[] bgSprites;



	// PRIVATE VARS..
	[HideInInspector]
	public   bool  GameOver  { get; set; }
	public   int   score     { get; set; }
	private  float m_distanceRan  = 0.0f;
	private  float m_startPosX    = 0.0f;
	private  bool  unlockedAch1 = false;
	private  bool  unlockedAch2 = false;
	private  bool  unlockedAch3 = false;
	private  bool  unlockedAch4 = false;
	private  bool  unlockedAch5 = false;

	void Awake() {
		
		if(_instance != null)
		{
			Destroy(this);
			return;
		}

		_instance = this;
		m_startPosX = m_SpanwerTrigger.position.x;
		GameOver = true;
	}

	// Use this for initialization
	void Start () {

		Screen.sleepTimeout = SleepTimeout.NeverSleep;

		unlockedAch1 = PlayerPrefs.GetInt ("unlockedAch1",0) == 1 ? true : false; //Standard 0 or 1 
		unlockedAch2 = PlayerPrefs.GetInt ("unlockedAch2",0) == 1 ? true : false; //Standard 0 or 1 
		unlockedAch3 = PlayerPrefs.GetInt ("unlockedAch3",0) == 1 ? true : false; //Standard 0 or 1 
		unlockedAch4 = PlayerPrefs.GetInt ("unlockedAch4",0) == 1 ? true : false; //Standard 0 or 1 
		unlockedAch5 = PlayerPrefs.GetInt ("unlockedAch5",0) == 1 ? true : false; //Standard 0 or 1 

		score = 0;
		if (scoreUi)
			scoreUi.text = score.ToString ();

		int lastScore = PlayerPrefs.GetInt("TOPSCORE");
		if (topScoreUi)
			topScoreUi.text = lastScore.ToString ();

		PreCreate (10); //Precreate 10

		//Object Spawner..
		StartCoroutine (StartGame ());
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.Escape)) //Android BackButton
		{ 
			Application.Quit(); 
		}

		if (GameOver)
			return;

		//Distance.. 
		m_distanceRan = m_SpanwerTrigger.position.x;
		//Increase Score..
		scoreUi.text = ""+score;
		//Unlock Achievement
		CheckAchievement();
	}



	//Check if Achivement to unlock..
	void CheckAchievement()
	{
		if (score == 5 && !unlockedAch1) {
			PlayerPrefs.SetInt ("unlockedAch1", 1);
			LeaderBoard.mInstance.UnlockAchievement (GPGSIds.achievement_scream_power_5);
		} 
		else if (score == 10 && !unlockedAch2) {
			PlayerPrefs.SetInt ("unlockedAch2", 1);
			LeaderBoard.mInstance.UnlockAchievement (GPGSIds.achievement_scream_power_10);	
		}
		else if (score == 15 && !unlockedAch3) {
			PlayerPrefs.SetInt ("unlockedAch3", 1);
			LeaderBoard.mInstance.UnlockAchievement (GPGSIds.achievement_scream_power_15);	
		}
		else if (score == 20 && !unlockedAch4) {
			PlayerPrefs.SetInt ("unlockedAch4", 1);
			LeaderBoard.mInstance.UnlockAchievement (GPGSIds.achievement_scream_power_20);	
		}
		else if (score == 25 && !unlockedAch5) {
			PlayerPrefs.SetInt ("unlockedAch5", 1);
			LeaderBoard.mInstance.UnlockAchievement (GPGSIds.achievement_scream_power_25);	
		}
	}


	float distIn  = 3f;
	float lastDist= 0.0f;
	IEnumerator StartGame()
	{
		while(!GameOver)
		{
			distIn = Random.Range (2.8f, 3.6f);

			while (Vector2.Distance (new Vector2 (lastDist, 0f), new Vector2 (m_distanceRan, 0f)) < distIn) {
				yield return null;
				//Debug.Log ("GameManager.cs -> StartGame - Waiting Distance:" + distIn);
				if (GameOver)
					break;
			}

			//Get all into the Local Temp
			List<GameObject> m_stagePrefabsTmp = new List<GameObject>();
			for(int index = 0; index < m_stageSpawnPool.Count; index++)
				m_stagePrefabsTmp.Add(m_stageSpawnPool[index]);

			// remove whatever is in use
			for(int sIndex = m_stagePrefabsTmp.Count - 1; sIndex >= 0; sIndex--)
			{
				if(m_stagePrefabsTmp[sIndex].activeInHierarchy)
					m_stagePrefabsTmp.RemoveAt(sIndex);
			}

			int objIndex = 0;
			objIndex  = Random.Range(0, m_stagePrefabsTmp.Count);

			if (m_stagePrefabsTmp.Count > 0) {
				GameObject go = m_stagePrefabsTmp [objIndex];
				go.transform.position = new Vector2 (m_SpanwerTrigger.position.x, go.transform.position.y); 
				go.SetActive (true);

				//Last..
				lastDist = m_SpanwerTrigger.position.x;
				m_stageSpawnPoolUsed.Add (go);

			} else {
				//Create more..
				PreCreate(5);
			}

			m_stagePrefabsTmp.Clear();
			yield return null;
		}

		//Debug.Log ("GameManager.cs -> StartGame = exit object spawning loop");
	}


	public void Reset()
	{
		//Save Score.. 
		int lastScore = PlayerPrefs.GetInt("TOPSCORE");
		if (score > lastScore) {
			PlayerPrefs.SetInt ("TOPSCORE", score);
			if (topScoreUi)
				topScoreUi.text = score.ToString ();
		}

		//RESET..
		lastDist = 0f;
		score = 0;

		//Disable all used Platforms Again..
		for (int i = 0; i < m_stageSpawnPoolUsed.Count; i++) {
			if (m_stageSpawnPoolUsed [i].activeInHierarchy)
				m_stageSpawnPoolUsed [i].SetActive (false);
		}

		GameOver = false;


		//Reset BG Sprite..
		if (bgRenderer) {
			bgRenderer.sprite = bgSprites [Random.Range (0, bgSprites.Length)];
		}

		//Object Spawner..
		StartCoroutine (StartGame ());
		Debug.Log ("GameManager.cs -> Rest");
	}

	public void PreCreate(int _num) {

		// --> Pre-create STAGES 
		for(int sIndex = 0; sIndex < _num; sIndex++)
		{
			int ranNum = Random.Range (0, m_stagePrefabs.Length);

			GameObject obj = Instantiate( m_stagePrefabs[ranNum], m_stagePrefabs[ranNum].transform.position, m_stagePrefabs[ranNum].transform.rotation ) as GameObject;
			//Set to Parent..
			obj.transform.SetParent( m_stageParentGroup );
			obj.SetActive (false);

			m_stageSpawnPool.Add(obj);
		}

	}

	private bool show = true;
	public void ShowHideDetail()
	{
		if (show) {
			show = false;
			panelDetail.GetComponent<Animator> ().SetTrigger ("in");
		} else {
			show = true;
			panelDetail.GetComponent<Animator> ().SetTrigger ("out");
		}
	}

	//Unity ADS..
	public void ShowAd()
	{
		if (Advertisement.IsReady())
		{
			Advertisement.Show();
		}
	}

}
