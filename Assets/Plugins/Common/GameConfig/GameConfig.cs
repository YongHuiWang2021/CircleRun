using System.Collections.Generic;
using UnityEngine;

namespace Plugins.Common.GameConfig
{
    public class GameConfig
    {
        static int mUsingProductIdx = 0;

        public static int UsingProductIdx
        {
            get
            {
                return mUsingProductIdx;
            }

           
        }

        public static readonly List<string> ProductNams = new List<string>()
        {
            "RunInCircle",
            "Client",
            "Client1",
            "CircleRun",
            "CircleRun1"
        };
        public static readonly List<string> UnityADSGameIDs = new List<string>()
        {
            "4708201",
            "4706479",
            "4707081",
            "4637737",
            "4739891"
        };

        public static string UnityGameidIdxKey = "UnityGameidIdx";
    }
}