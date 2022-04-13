using System;
using Scripts.Common.Factory;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

namespace XD.Scripts.RV.ADS
{
    public abstract class UnityADSItem : ADSItem, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        
        string gameId = "4637737";
     
       protected string _adUnitId = null; // This will remain null for unsupported platforms
//"4637737"
  //      "4637736"IOS
        private bool mIsRead = false;
    
      public override ADSType GetAdsType()
      {
          return ADSType.Unity;
      }

      public abstract string GetUnitId();

      public override bool IsReady()
      {
          return mIsRead;
      }

      public override void Show()
      {
          Advertisement.Show(_adUnitId, this);
      }
    

        // Load content to the Ad Unit:
        public override void LoadAd()
        {
            mIsRead = false;
           
            // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
            Debug.Log("Loading Ad: " + _adUnitId);
            Advertisement.Load(_adUnitId, this);
        }
     
        // If the ad successfully loads, add a listener to the button and enable it:
        public void OnUnityAdsAdLoaded(string adUnitId)
        {
            Debug.Log("Ad Loaded: " + adUnitId);
            mIsRead = true;

        }
        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
           
        }
        // Implement the Show Listener's OnUnityAdsShowComplete callback method to determine if the user gets a reward:
        public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
        {
            if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
            {
                Debug.Log("Unity Ads Rewarded Ad Completed");
                // Grant a reward.
                // Load another ad:
                Advertisement.Load(_adUnitId, this);
            }
        }
     
        // Implement Load and Show Listener error callbacks:
        public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
        {
            Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
            // Use the error details to determine whether to try to load another ad.
        }
     
        public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
        {
            Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
            // Use the error details to determine whether to try to load another ad.
        }

        public void OnUnityAdsShowStart(string adUnitId)
        {
            Debug.Log($"OnUnityAdsShowStart {adUnitId}");
        }

        public void OnUnityAdsShowClick(string adUnitId)
        {
            
        }
        
    }
}