using System;
using Scripts.Common.Factory;
using UnityEngine;
using UnityEngine.Advertisements;
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
            XDDebug.Log("UnityADSManager . InitializeAds");
            ADSSystemData data = Resources.Load<ADSSystemData>("ADSSystemData");
            XDDebug.Log("InitializeAds :: "+data.UnityADSGameID.ToString());
            Advertisement.Initialize(data.UnityADSGameID.ToString(),false,false,this);
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