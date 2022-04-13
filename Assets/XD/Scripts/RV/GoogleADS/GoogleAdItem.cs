using System;

using UnityEngine;

namespace XD.Scripts.RV
{
    public interface IGoogleAdItem
    {
        void LoadAd();
        void Show();
    }

    
    public abstract class GoogleAdItem : IGoogleAdItem
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
        public abstract void LoadAd();
        public abstract void Show();
    

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