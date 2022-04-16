using System;

namespace XD.Scripts.RV.ADS
{
    public enum ADSType
    {
        Unity,
        Google,
    }

    public abstract class ADSItem
    {
        public abstract ADSType GetAdsType();
        public Action OnADClose =null;
   
        public int ShowCount = 0;
        public abstract bool IsReady();
        public abstract void LoadAd();
        public abstract void Show();
    }
}