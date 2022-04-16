using UnityEngine;

namespace XD.Scripts
{
    [CreateAssetMenu(fileName ="SystemParam",menuName = ("XD/AssetsData/SystemParam"))]
    public class SystemParam   : ScriptableObject
    {
        private static SystemParam mInstance = null;
        public static SystemParam Instance
        {
            get
            {
                if (mInstance == null)
                {
                    mInstance = Resources.Load<SystemParam>("AssetsData/SystemParam");
                }

                return mInstance;
            }
        }
        public Color GameColor = Color.red;
        public float Damp = 2.0f;
        public float MoveSpeed = 5.0f;
        [SerializeField] GameObject[] _Guanka = null;
        public  Color LineColor;
        public GameObject GetGuanKa(int Idex)
        {
            int idx = Idex % _Guanka.Length;
            return _Guanka[idx];
        }
        public int GetGuanKaCount()
        {
            
            return _Guanka.Length;
        }
        
        
        [SerializeField] private int _UnityADSAndroidGameID;
        [SerializeField] private int _UnityADSIOSGameID;

        public void SetAndroidGameID(int id)
        {
            _UnityADSAndroidGameID = id;
        }

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

        public SystemParam Clone()
        {
           return new SystemParam()
           {
               GameColor = this.GameColor,
               Damp = this.Damp,
               MoveSpeed = this.MoveSpeed,
               _Guanka = this._Guanka,
               LineColor = this.LineColor,
               _UnityADSIOSGameID = this._UnityADSIOSGameID,
               _UnityADSAndroidGameID = this._UnityADSAndroidGameID,
           };
        }
    }
}