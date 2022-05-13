using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using XD.Scripts;
using XD.Scripts.RV;

public class Game : MonoBehaviour
{
    [SerializeField] private Button _PlayBtn = null;
    [SerializeField] private Button _ExitBtn = null;
    [SerializeField] private TextMeshProUGUI _PlayTag;
    [SerializeField] private TextMeshProUGUI _ExitTag;
    private Image mPlayImage = null;
    private Image mExitImage = null;

    void Start()
    {
        mPlayImage = _PlayBtn.GetComponent<Image>();
        mExitImage = _ExitBtn.GetComponent<Image>();
        
        ADSSystemData data = Resources.Load<ADSSystemData>("ADSSystemData");
        _PlayTag.text = data.Tag.ToString();
        _ExitTag.text = data.Tag.ToString();
        
        
        _PlayBtn.onClick.AddListener(PlayADS); 
        
        _ExitBtn.onClick.AddListener(Exit); 
    }

    private float mLastClickTime = 0;
    private void PlayADS()
    {
        GameRVManager.Instance.ShowRV();
        mPlayImage.color = Color.yellow;
        mLastClickTime = CCRandom.Range(30,300);
    }

    private void Exit()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        Color UsingColor = GameRVManager.Instance.RVIsReady ? Color.green :Color.clear ;
      
        Color ExitColor = Color.blue;
        if (GameRVManager.Instance.PlayCount > 9)
        {
            ExitColor = Color.red;
            Application.Quit();
        }

        mExitImage.color = ExitColor;
        if (mLastClickTime > 0 && mLastClickTime!= -9)
        {
            mLastClickTime -= Time.deltaTime;
            UsingColor = Color.yellow;
        }
        else
        {
            mLastClickTime = -9;
        }
        mPlayImage.color = UsingColor;
    }
}
