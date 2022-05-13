using System;

using UnityEngine;
using XD.Scripts.RV.ADS;

namespace XD.Scripts.RV
{
    public interface IGoogleAdItem
    {
        void LoadAd();
        void Show();
    }

    
    public abstract class GoogleAdItem : ADSItem
    {
        public bool Loaded
        {
            get
            {
                return IsLoaded();
            }
        }
        public int ShowCount = 0;
        public Action OnADClose =null;
        public Action OnADLoaded =null;
        public abstract bool IsLoaded();
        public abstract override void LoadAd();
        public abstract override void Show();
    

        public void HandleRewardedAdLoaded(object sender, EventArgs args)
        {
            Debug.Log("HandleRewardedAdLoaded event received");
          
            OnADLoaded?.Invoke();
        }

        /*
        public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
        {
            Debug.Log(
                "HandleRewardedAdFailedToLoad event received with message: "
                + args.Message);
        }

        public void HandleRewardedAdOpening(object sender, EventArgs args)
        {
            Debug.Log("HandleRewardedAdOpening event received");
        }

        public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
        {
            Debug.Log(
                "HandleRewardedAdFailedToShow event received with message: "
                + args.Message);
        }

        public void HandleRewardedAdClosed(object sender, EventArgs args)
        {
            Debug.Log("HandleRewardedAdClosed event received");
            ++ShowCount;
            OnADClose?.Invoke();
        }

        public void HandleUserEarnedReward(object sender, Reward args)
        {
            string type = args.Type;
            double amount = args.Amount;
            Debug.Log(
                "HandleRewardedAdRewarded event received for "
                + amount.ToString() + " " + type);
        }*/
    }
}