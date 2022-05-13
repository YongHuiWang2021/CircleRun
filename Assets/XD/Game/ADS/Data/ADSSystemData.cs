using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName ="ADSSystemData",menuName = ("XD/AssetsData/ADSSystemData"))]
public class ADSSystemData :    ScriptableObject
{
    [SerializeField] private int _UnityADSAndroidGameID;
    [SerializeField] private int _UnityADSIOSGameID;
    [SerializeField] private Color _PlayerColor;
    [SerializeField] private Color _ExitColor;
    [SerializeField] private int _Tag = 0;
    public int UnityADSGameID
    {
        get
        {
#if UNITY_ANDROID
            return _UnityADSAndroidGameID;
#elif UNITY_IOS
                return _UnityADSIOSGameID;
#endif
            return _UnityADSAndroidGameID;
        }
            
    }

    public Color PlayerColor
    {
        get
        {
            return _PlayerColor;
        }
    }

    public Color ExitColor
    {
        get
        {
            return _ExitColor;
        }
    }

    public string Tag
    {
        get
        {
            return "OK";
        }
    }
}
