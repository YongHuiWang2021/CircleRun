
using UnityEngine;
using XD.Scripts.RV.ADS;

namespace XD.Scripts.RV
{
    public class GoogleInterAdItem : GoogleAdItem
    {
        public string UnitID = "ca-app-pub-9779546067757389/9157791798";
        /*private AdRequest adr = null;

        private InterstitialAd mGoogleAd = null;*/

        //  private RewardedAd mRewardedAd = null;
        public override void Show()
        {
            /*if (mGoogleAd != null)
                mGoogleAd.Show();
            Debug.Log("GoogleADS GoogleInterAdItem " +mGoogleAd.ToString());*/
            // mGoogleAd.Show();
        }

        public override bool IsLoaded()
        {
            /*if (mGoogleAd != null)
                return mGoogleAd.IsLoaded();*/
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
                mGoogleAd = new InterstitialAd(UnitID);
                mGoogleAd.LoadAd(adr);
                mGoogleAd.OnAdClosed += base.HandleRewardedAdClosed;
                mGoogleAd.OnAdLoaded += base.HandleRewardedAdLoaded;
                mGoogleAd.OnAdOpening += base.HandleRewardedAdOpening;
                mGoogleAd.OnAdFailedToLoad += (m,s) =>
                {
                    HandleRewardedAdFailedToLoad(m,new AdErrorEventArgs(){Message = s.Message});
                };
            }*/
        }
    }
}