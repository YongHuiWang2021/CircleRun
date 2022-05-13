using System;
using System.Collections.Generic;
using Scripts.Common.Factory;
using UnityEngine;
using XD.Core;
using XD.XDLog;

namespace XD.Scripts.RV
{
    public class GoogleADSManager : SingletonMonoBehaviour<GoogleADSManager>
    {
        public int Count {
            get
            {
                return  mItems.Count;
            }
        }

        public int InitSuccessCount
        {
            get
            {
                int ret = 0;
                foreach (var item in mItems)
                {
                    if (item.Loaded)
                    {
                        ++ret;
                    }
                }

                return ret;
            }
        }
        public Action ADClose = null;
        public bool InitOK = false;
        private List<string> UnitRewardIDs = new List<string>()
        {
            "ca-app-pub-9779546067757389/3674402962",
            "ca-app-pub-9779546067757389/7328876824"
            
        };
        private List<string> InterUnitIDs = new List<string>()
        {
            "ca-app-pub-9779546067757389/9157791798",
        };
        public void Init(Action initOk)
        {
            Debug.Log("GoogleADS Init");
           
            // Initialize the Google Mobile Ads SDK.
            /*for (int i = 0; i < InterUnitIDs.Count; i++)
            {
                GoogleInterAdItem item = new GoogleInterAdItem();
                item.UnitID = InterUnitIDs[i];
                // item.LoadAd();
                mItems.Add(item);
            }
            Debug.Log("GoogleADS Init1");
            for (int i = 0; i < UnitRewardIDs.Count; i++)
            {
                GoogleRVAdItem item = new GoogleRVAdItem();
                item.UnitID = UnitRewardIDs[i];
                // item.LoadAd();
                mItems.Add(item);
            }
            Debug.Log("GoogleADS Init2".AddHTMLColor(Color.green));*/
           
            
        }

        private void OnADLoaded()
        {
            
        }

        private void OnADClose()
        {
            ADClose?.Invoke();
        }

        public void Show()
        {
            Debug.Log("GoogleADS Show");
            List<GoogleAdItem> showItem = new List<GoogleAdItem>();
            foreach (var ii in mItems)
            {
                if (ii.Loaded)
                {
                    showItem.Add(ii);
                }
            }
            showItem.Sort(delegate(GoogleAdItem aItem, GoogleAdItem bItem)
            {
                return aItem.ShowCount - bItem.ShowCount;
            });
            var  showRVitem =showItem.GetRandomItem();
            if(showRVitem!=null)
                showRVitem.Show();
          
        }

        private List<GoogleAdItem> mItems = new List<GoogleAdItem>();
        protected override void Update()
        {
            base.Update();
            
        }

    
  
    }
}