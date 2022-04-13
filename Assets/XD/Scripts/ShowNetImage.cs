using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Circle_Clone_Template.Scripts
{
    public class ShowNetImage : MonoBehaviour
    {
        private Image _Image;
        private void Start()
        {
            _Image = GetComponent<Image>();
            StartCoroutine(DownLoad());
        }

        IEnumerator DownLoad()
        {
            string path = PathUtils.GetStreamingAssetsPath("baozi1_1.png");
            WWW www = new WWW(path);
            yield return www;
            if (string.IsNullOrEmpty(www.error))
            {
                _Image.sprite = Sprite.Create(www.texture,new Rect(0,0,400,400),new Vector2(0.5f,0.5f));  
            }
            else
            {
                Debug.LogError("ERROE : "+ www.error);
            }
        }
    }
}