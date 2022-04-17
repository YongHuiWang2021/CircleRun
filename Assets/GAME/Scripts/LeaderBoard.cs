using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#if UNITY_ANDROID
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.Quests;
#endif

using UnityEngine.SocialPlatforms;

public class LeaderBoard : MonoBehaviour {

	public static LeaderBoard mInstance;
	[Header("IOS")]
	public string iosLeaderBoard = "leaderboardID";

	#if UNITY_ANDROID
	//REFER TO :https://github.com/playgameservices/play-games-plugin-for-unity
	private string googleLeaderBoardId = GPGSIds.leaderboard_scream_chicken_jumper;  
	#endif

	private string leaderBoardIdLocal;

	void Awake()
	{
		//If we don't currently have a game control...
		if (mInstance == null)
			//...set this one to be it...
			mInstance = this;
		//...otherwise...
		else if(mInstance != this)
			//...destroy this one because it is a duplicate.
			Destroy (gameObject);
	}

	void Start()
	{
		DontDestroyOnLoad (gameObject);

		#if UNITY_ANDROID
			leaderBoardIdLocal = googleLeaderBoardId;
		#elif UNITY_IPHONE
			leaderBoardIdLocal = iosLeaderBoard;
		#endif

		if (!playerIsLoggedIn ()) {
			LoginIn ();
		}
	}


	public void LoginIn()
	{
		if (!Social.localUser.authenticated) {

			#if UNITY_ANDROID
			// recommended for debugging:
			PlayGamesPlatform.DebugLogEnabled = false;
			// Activate the Google Play Games platform
			PlayGamesPlatform.Activate ();
			#endif 

			// authenticate user:
			Social.localUser.Authenticate ((bool success) => {
				// handle success or failure
				if (success) {
					print ("You have successfully logged in");
				} else {
					print ("Login failed..");
				}
			});
		}
	}
		
	public string playerName()
	{
		if (playerIsLoggedIn ())
			return Social.localUser.userName;
		else
			return "";
	}

	public bool playerIsLoggedIn()
	{
		return Social.localUser.authenticated;
	}
		

	public void GetUserScores()
	{
		// get the user ids
		Dictionary<int, int> usersScores = new Dictionary<int, int>();

		ILeaderboard leaderboard = Social.CreateLeaderboard();
		leaderboard.id = leaderBoardIdLocal;
		//leaderboard.range = new Range (100, 10);

		leaderboard.LoadScores (result => {              
			Debug.Log("Received " + leaderboard.scores.Length + " scores");

			foreach (IScore score in leaderboard.scores)
				usersScores.Add(score.rank, int.Parse(score.formattedValue));

		});
	}

	public void UnlockAchievement(string _id)
	{
		if (!playerIsLoggedIn ())
			return;

		// unlock achievement (achievement ID "Cfjewijawiu_QA")
		Social.ReportProgress(_id, 100.0f, (bool success) => {
			// handle success or failure
			if( success )
			{
				AudioManager._instance.PlayRecrod();
			}
			else
			{
				print ("achievement failed..");
			}
		});
	}

	public void PostScore(int _score)
	{
		// post score 12345 to leaderboard ID "Cfji293fjsie_QA")
		Social.ReportScore(_score, leaderBoardIdLocal, (bool success) => {
			// handle success or failure
			if(success)
				print("Score Posted..");
		});
	}

	public void ShowLeaderUI()
	{
		print ("Show Leaderboard..");
		if (!playerIsLoggedIn ())
			LoginIn ();

		#if UNITY_ANDROID
		// show leaderboard UI
		PlayGamesPlatform.Instance.ShowLeaderboardUI(googleLeaderBoardId);
		#elif UNITY_IPHONE
		// show leaderboard UI
		//UnityEngine.SocialPlatforms.GameCenter.GameCenterPlatform.ShowLeaderboardUI(iosLeaderBoard, TimeScope.AllTime);
		Social.ShowLeaderboardUI ();
		#endif

	}
		
	public void ShowAchievement()
	{
		// show achievements UI
		Social.ShowAchievementsUI();
	}

	#if UNITY_ANDROID
	public void IncrementEvent(string id)
	{
		// Increments the event with Id "YOUR_EVENT_ID" by 1
		PlayGamesPlatform.Instance.Events.IncrementEvent(id, 1);
	}

	public void ViewQuests()
	{
		Debug.Log("clicked:ViewQuests");
		PlayGamesPlatform.Instance.Quests.ShowAllQuestsUI(
			(QuestUiResult result, IQuest quest, IQuestMilestone milestone) =>
			{
				if (result == QuestUiResult.UserRequestsQuestAcceptance)
				{
					Debug.Log("User Requests Quest Acceptance");
					AcceptQuest(quest);
				}

				if (result == QuestUiResult.UserRequestsMilestoneClaiming)
				{
					Debug.Log("User Requests Milestone Claim");
					ClaimMilestone(milestone);
				}
			});
	}


	private void AcceptQuest(IQuest toAccept)
	{
		PlayGamesPlatform.Instance.Quests.Accept(toAccept,
			(QuestAcceptStatus status, IQuest quest) => {
				if (status == QuestAcceptStatus.Success)
				{
					// ...
				}
			});
	}

	private void ClaimMilestone(IQuestMilestone toClaim)
	{
		PlayGamesPlatform.Instance.Quests.ClaimMilestone(toClaim,
			(QuestClaimMilestoneStatus status, IQuest quest, IQuestMilestone milestone) => {
				if (status == QuestClaimMilestoneStatus.Success)
				{
					/* Read json from the Google.. Need to import the Json API
					string reward = System.Text.Encoding.UTF8.GetString(quest.Milestone.CompletionRewardData);
					JSONObject rewardJson = new JSONObject(reward);
					int gems = int.Parse(rewardJson.GetField("Gems").ToString());
					PlayerPrefs.SetInt("Gems", PlayerPrefs.GetInt("Gems", 0) + gems);
					*/
				
					//SoundManager.Instance.PlayGameCollectible ();
					//ScoreManager.SaveCoin  (ScoreManager.GetCoins() + 500);

				}
			});
	}
	#endif 
}
