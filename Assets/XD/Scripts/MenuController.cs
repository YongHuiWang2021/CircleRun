using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using XD.Scripts;
using XD.Scripts.RV;

public class MenuController : MonoBehaviour {
    //Fade volor
    public Color fadeColor = Color.white;
    public Image soundButtonStart;
    public Image soundButtonEnd;
    public Sprite soundOn;
    public Sprite soundOff;

    // Use this for initialization
    void Start () {
        //Set looks for the button at start
        SetSoundButton();
    }
    //Put your leaderboard code here
    public void Leaderboard() {
        Debug.Log("Leaderboard Goes here , Click to Open In IDE");
    }

    public void Replay()
    {
        Initiate.Begin(Application.loadedLevelName, SystemParam.Instance.GameColor, SystemParam.Instance.Damp);
    }

    //Restart the Game
    public void ReplayWithRV() {

#if UNITY_EDITOR
        Initiate.ThenPlay();
        return;
#endif
        if (GameRVManager.Instance.RVIsReady)
        {
            GameRVManager.Instance.ShowRV();
            GameRVManager.Instance.LookOver = (r) =>
            {
                Debug.Log("LookOver :" + r);
                if (r == 1)
                {
                    Initiate.ThenPlay();
                }
                else
                {
                    Debug.Log("LookOver  Not Finish");
                }

            };
        }
        else
        {
            Debug.Log(" GameRV Is Not Ready");
          
        }
        // Initiate.Begin(Application.loadedLevelName, SystemParam.Instance.GameColor, SystemParam.Instance.Damp);
    }

    public void ChangeSound() {
        //Turn sound on or off

        if (PlayerPrefs.GetInt("Sound", 1) == 1) {
            PlayerPrefs.SetInt("Sound", 0);
        }
        else if (PlayerPrefs.GetInt("Sound", 1) == 0)
        {
            PlayerPrefs.SetInt("Sound", 1);
        }

        SetSoundButton();

    }

    //Set how the audio button looks
    void SetSoundButton() {

        if (!soundOn || !soundOff || !soundButtonEnd || !soundButtonStart)
            Debug.LogError("Please Assign all the variables");

        if (PlayerPrefs.GetInt("Sound", 1) == 1)
        {
            AudioListener.volume = 1.0f;
            soundButtonStart.sprite = soundOn;
            soundButtonEnd.sprite = soundOn;
        }
        else {
            AudioListener.volume = 0.0f;
            soundButtonStart.sprite = soundOff;
            soundButtonEnd.sprite = soundOff;
        }
    }

}
