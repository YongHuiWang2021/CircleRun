using System;
using Scripts.Common.Factory;
using UnityEngine;

namespace XD.XDLog
{
    public class XDDebugListener  : SingletonMonoBehaviour<XDDebugListener>
    {
        private void OnApplicationQuit()
        {
            XD.XDLog.XDDebug.SaveLog();
        }
    }
}