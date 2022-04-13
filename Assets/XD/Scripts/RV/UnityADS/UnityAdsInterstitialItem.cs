namespace XD.Scripts.RV.ADS
{
    public class UnityAdsInterstitialItem:UnityADSItem
    {
#if UNITY_ANDROID
        string _UnitId = "Interstitial_Android";
#else
        string _UnitId = "Interstitial_iOS";
#endif
        public override string GetUnitId()
        {
            return _UnitId;
        }

    }
}