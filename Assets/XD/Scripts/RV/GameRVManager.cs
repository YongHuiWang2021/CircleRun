using System;
using System.Collections;
using System.Collections.Generic;
using Scripts.Common.Factory;
using UnityEngine;
using UnityEngine.Advertisements;
using XD.Core;
using XD.Scripts.RV.ADS;
using Random = System.Random;

namespace XD.Scripts.RV
{
    public class GameRVManager : SingletonMonoBehaviour<GameRVManager>
    {
        public Action<int> LookOver = null;
       
        private List<ADSItem> mWaitingforPlayRV = new List<ADSItem>();
        public void Init()
        {
            InitADSItems();
            InitADS();

        }

        protected void LoadADS(ADSType tType)
        {
            foreach (var item in mWaitingforPlayRV)
            {
                if (item.GetAdsType() == tType)
                {
                    item.LoadAd();
                }
            }
        }

        protected void InitADS()
        {
            UnityADSManager.Instance.Init(() => { LoadADS(ADSType.Unity);});
        }

        protected void InitADSItems()
        {
            mWaitingforPlayRV.Add( new UnityAdsRewardItem());
            mWaitingforPlayRV.Add( new UnityAdsInterstitialItem());
        }

        private void AsClose()
        {
            LookBack(0);
        }

        private bool mLookOver = false;
        private void LookBack(int ret)
        {
            mLookOver = true;
            if (LookOver != null)
            {
                LookOver(ret);
            }
            Debug.Log(LookOver);
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
           
        }

        public int CanUseCount
        {
            get
            {
                int ret = 0;
                
                return ret;
            }
        }

        public bool RVIsReady
        {
            get
            {
                
                return Advertisement.isInitialized ;
            }
        }

        public void ShowRV()
        {
           int idx= CCRandom.Range(0, mWaitingforPlayRV.Count);
           if (idx < mWaitingforPlayRV.Count)
           {
               mWaitingforPlayRV[idx].Show();
           }
           
           
        }

        private void PlayAgain()
        {
            ShowRV();
        }

        IEnumerator Waite(Action action)
        {
            yield return new WaitForSeconds(2);
            action.Invoke();
        }

        protected override void Update()
        {
            base.Update();
            if (mLookOver)
            {
                if (RVIsReady)
                {
                    mLookOver = false;
                    StartCoroutine(Waite(() =>
                    {
                        PlayAgain();
                    }));
                }
            }
        }
        
    }
}