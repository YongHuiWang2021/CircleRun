using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace XD.Scripts
{
    public class LanguageBind : MonoBehaviour
    {
        [SerializeField] private string _Key;
        private TextMeshProUGUI _Text;
        private Text _Text1;
        private void Awake()
        {
            if (_Text == null)
            {
                _Text = GetComponent<TextMeshProUGUI>();
            } 
            if (_Text1 == null)
            {
                _Text1 = GetComponent<Text>();
            }
            
            if (string.IsNullOrEmpty(_Key))
            {
                if (_Text != null)  _Key = _Text.text;
                if (_Text1 != null)  _Key = _Text1.text;
            }

            if (!string.IsNullOrEmpty(_Key))
            {
                string ret = Language.Instance.GetLanguage(_Key);
                if (!string.IsNullOrEmpty(ret))
                {
                    if (_Text != null)  _Text .text= ret;
                    if (_Text1 != null) _Text1 .text= ret;
                }
            }
        }
    }
}