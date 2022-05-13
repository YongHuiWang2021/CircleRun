
using UnityEngine;
using XD.Scripts.RV.ADS;

namespace XD.Scripts.RV
{
      
        
        public class GoogleRVAdItem:GoogleAdItem
        {
            public string UnitID =   "ca-app-pub-9779546067757389/3674402962";
            /*private AdRequest adr = null; 
            
       //     private InterstitialAd mGoogleAd = null;
            private RewardedAd mRewardedAd = null;*/
            public override void Show()
            {
                /*if(mRewardedAd!=null)
                    mRewardedAd.Show();
                Debug.Log("GoogleADS GoogleRVAdItem " +mRewardedAd.ToString());*/
               // mGoogleAd.Show();
            }

            public override bool IsLoaded()
            {
                /*if (mRewardedAd != null)
                    return mRewardedAd.IsLoaded();*/
                return false;
            }

            public override ADSType GetAdsType()
            {
                return ADSType.Google;
            }

            public override bool IsReady()
            {
                throw new System.NotImplementedException();
            }

            public override void LoadAd()
            {
                /*if (adr == null)
                {
                    AdRequest.Builder builder = new AdRequest.Builder();
                    adr = builder.Build();
                    mRewardedAd = new RewardedAd(UnitID);
                    mRewardedAd.LoadAd(adr);
                    mRewardedAd.OnAdClosed += base.HandleRewardedAdClosed;
                    mRewardedAd.OnAdLoaded += base.HandleRewardedAdLoaded;
                    mRewardedAd.OnAdOpening += base.HandleRewardedAdOpening;
                    mRewardedAd.OnAdFailedToShow += base.HandleRewardedAdFailedToShow;
                    mRewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;

                }*/
            }
        }
}