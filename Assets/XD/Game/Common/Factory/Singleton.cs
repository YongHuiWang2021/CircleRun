using System;
using System.ComponentModel;
using UnityEngine;

namespace Scripts.Common.Factory
{
    public class Singleton<T> where T : new()
    {
        private static T instance ;

        public static T Instance
        {
            get
            {
                if (instance == null)
                    instance = new T();
                return instance;

            }
            

        }
    }

    public class SingletonMonoBehaviourAssets<T> : MonoBehaviour 
    {
        private static T instance ;

        public static T Instance
        {
            get
            {
                return instance;
            }
        }

        private void Awake()
        {
            instance = this.GetComponent<T>();
        }

        private void OnDestroy()
        {
            instance = default(T);
        }
    }

    public interface ISingleton
    {
        void InitData(Action InitOK);
    }
    public class SingletonMonoBehaviour<T> :  MonoBehaviour,ISingleton where T : MonoBehaviour,ISingleton
    {
        private static T _instance;

        private static object _lock = new object();

        public static T Instance
        {
            get
            {
    
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = (T)FindObjectOfType(typeof(T));

                        if (FindObjectsOfType(typeof(T)).Length > 1)
                        {
                            return _instance;
                        }

                        if (_instance == null)
                        {
                            GameObject singleton = new GameObject();
                            _instance = singleton.AddComponent<T>();
                            singleton.name = "(singleton) " + typeof(T).ToString();
                           
                            DontDestroyOnLoad(singleton);
                         
                        }
                    }

                    return _instance;
                }
            }
        }

        private static bool applicationIsQuitting = false;

        public virtual void InitData(Action InitOK)
        {
            
        }

        public virtual void OnDestroy()
        {
            applicationIsQuitting = true;
        }

        public static bool IsDestroy()
        {
            return applicationIsQuitting;
        }

        protected virtual void Update()
        {
            
        }
    }

}