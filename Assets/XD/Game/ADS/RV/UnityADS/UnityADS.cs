using System;
using Scripts.Common.Factory;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using XD.XDLog;

namespace XD.Scripts.RV.ADS
{
    public abstract class UnityADSItem : ADSItem, IUnityAdsLoadListener, IUnityAdsShowListener
    {
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
          Advertisement.Show(GetUnitId(), this);
      }
    

        // Load content to the Ad Unit:
        public override void LoadAd()
        {
            mIsRead = false;
           
            // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
            Debug.Log("Loading Ad: " + GetUnitId());
            XDDebug.Log("Loading Ad: " + GetUnitId());
            Advertisement.Load(GetUnitId(), this);
        }
     
        // If the ad successfully loads, add a listener to the button and enable it:
        public void OnUnityAdsAdLoaded(string adUnitId)
        {
            Debug.Log("Ad Loaded: " + adUnitId);
            XDDebug.Log("Ad Loaded: " + GetUnitId());
            mIsRead = true;

        }
        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
           
        }
        // Implement the Show Listener's OnUnityAdsShowComplete callback method to determine if the user gets a reward:
        public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
        {
            XDDebug.Log("OnUnityAdsShowComplete : " + adUnitId + " : "+showCompletionState);
            OnADClose?.Invoke();
            if (adUnitId.Equals(GetUnitId()) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
            {
              
                // Grant a reward.
                // Load another ad:
                Advertisement.Load(GetUnitId(), this);
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
            XDDebug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
            Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
            // Use the error details to determine whether to try to load another ad.
        }

        public void OnUnityAdsShowStart(string adUnitId)
        {
            XDDebug.Log($"OnUnityAdsShowStart {adUnitId}");
            Debug.Log($"OnUnityAdsShowStart {adUnitId}");
        }

        public void OnUnityAdsShowClick(string adUnitId)
        {
            
        }
        
    }
}