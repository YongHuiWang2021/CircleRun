using System;
using Plugins.Package.LogWriterToLocal;
using Scripts.Common.Factory;
using UnityEngine;
using UnityEngine.Advertisements;

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
            Advertisement.Initialize(SystemParam.Instance.UnityADSGameID.ToString(),false,false,this);
        }

        public void OnInitializationComplete()
        {
            mInitBack?.Invoke();
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
           Debug.Log(error+"  :: "+message);
        }
    

    }
}