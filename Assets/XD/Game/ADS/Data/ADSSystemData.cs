using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="SystemParam",menuName = ("XD/AssetsData/ADSSystemData"))]
public class ADSSystemData :    ScriptableObject
{
    [SerializeField] private int _UnityADSAndroidGameID;
    [SerializeField] private int _UnityADSIOSGameID;
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
}
