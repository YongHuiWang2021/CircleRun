using System;
using Plugins.Common.GameConfig;
using Scripts.Common.Factory;
using UnityEngine;
using UnityEngine.Advertisements;
using XD.Core;
using XD.XDLog;

namespace XD.Scripts.RV.ADS
{
    public class UnityADSManager : SingletonMonoBehaviour<UnityADSManager>,IUnityAdsInitializationListener
    {
        protected Action mInitBack = null;

        public void Init( Action initBack)
        {
            mInitBack = initBack;
            InitializeAds();
        }

        protected virtual void InitializeAds()
        {
            int GameIDIDx = 0;
            string GameID ="";
            if (PlayerPrefs.HasKey(GameConfig.UnityGameidIdxKey))
            {
               GameIDIDx = PlayerPrefs.GetInt(GameConfig.UnityGameidIdxKey)+1;
                if (GameIDIDx >= GameConfig.UnityADSGameIDs.Count ||GameIDIDx<0)
                {
                    GameIDIDx = 0;
                }
            }
            else
            {
                PlayerPrefs.SetInt(GameConfig.UnityGameidIdxKey,GameIDIDx);
            }

            GameID = GameConfig.UnityADSGameIDs[GameIDIDx];
            /*XDDebug.Log("UnityADSManager . InitializeAds");
            ADSSystemData data = Resources.Load<ADSSystemData>("ADSSystemData");*/
            XDDebug.Log("InitializeAds :: "+GameID);
            Advertisement.Initialize(GameID,false,false,this);
        }

        public void OnInitializationComplete()
        {
            mInitBack?.Invoke();
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            XDDebug.Log(error+"  :: "+message);
           Debug.Log(error+"  :: "+message);
        }
    

    }
}