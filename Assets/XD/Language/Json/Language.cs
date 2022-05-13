using System;
using System.Collections.Generic;
using System.Management.Instrumentation;
using LitJson;
using Scripts.Common.Factory;
using UnityEngine;
using XD.XDLog;

namespace XD.Scripts
{
    public class LanguageVO
    {

        public int ID { get; set; }
       
        public string Key { get; set; }
     
        public string EN { get; set; }
        public string CN { get; set; }
    }

    public enum LanguageTYpe
    {
        EN,CN,
    }
    public class Language : MonoBehaviour
    {
        [SerializeField] private TextAsset _LanguageData;
        [SerializeField] private LanguageTYpe _LanguageType = LanguageTYpe.CN;
        public static Language Instance { get; private set; }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            Init();
        }

        private Dictionary<string, string> mLanguageData = new Dictionary<string, string>();

        public  string GetLanguage(string key)
        {
            string ret = "";
            mLanguageData.TryGetValue(key, out ret);
            return ret;
        }

        private string GetLangaugeData(LanguageVO vo)
        {
            switch (_LanguageType)
            {
                case LanguageTYpe.CN:
                    return vo.CN;
                case LanguageTYpe.EN :
                    return vo.EN;
            }

            return "";
        }

        private void Init()
        {
            var josnData = JsonMapper.ToObject<List<LanguageVO>>(_LanguageData.text);
            foreach (var iVo in josnData)
            {
                Debug.Log(iVo.EN.AddHTMLColor(Color.green));
                Debug.Log(iVo.CN.AddHTMLColor(Color.yellow));
                mLanguageData[iVo.Key] = GetLangaugeData(iVo);
            }
        }
        
    }
}