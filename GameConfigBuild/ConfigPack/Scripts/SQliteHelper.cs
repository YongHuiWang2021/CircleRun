using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqliteDriver
{
    class SQliteHelper
    {

        private static SQliteHelper mInstance = null;
        public static SQliteHelper Instance
        {
            get
            {
//                 if (mInstance == null)
//                     mInstance = new SQliteHelper();
                return mInstance;
            }
        }
       
    }
}
