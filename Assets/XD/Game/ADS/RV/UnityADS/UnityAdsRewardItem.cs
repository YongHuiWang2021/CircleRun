using UnityEngine.Advertisements;

namespace XD.Scripts.RV.ADS
{
    public class UnityAdsRewardItem:UnityADSItem
    {
#if UNITY_ANDROID
        string _UnitId = "Rewarded_Android";
#else
        string _UnitId = "Rewarded_iOS";
#endif
      
        public override string GetUnitId()
        {
            return _UnitId;
        }

    }
}