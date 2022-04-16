using System.Collections.Generic;
using UnityEngine;

namespace Plugins.Common.GameConfig
{
    public class GameConfig
    {
        static int mUsingProductIdx = 3;

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
            "CircleRun"
        };
        public static readonly List<string> GameIDs = new List<string>()
        {
            "4708201",
            "4706478",
            "4707080",
            "4637737"
        };
    }
}