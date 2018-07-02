﻿namespace QFramework
{
    using UnityEngine;
    public abstract class MonoSingleton<T>:MonoBehaviour,ISingleton where T:MonoSingleton<T>
    {
        protected static T mInstance = null;
        public static T Instance
        {
            get

            {
                if (mInstance==null)
                {
                    mInstance = MonnoSingletonCreator.CreateMonoSingleton<T>();

                }
                return mInstance;
            }
            
        }
        public virtual void OnSingletonInit()
        {

        }
        public virtual void Dispose()
        {
            if (MonnoSingletonCreator.IsUnitTestMode)
            {
                var curTrans = transform;
                do
                {
                    var parent = curTrans.parent;
                    DestroyImmediate(curTrans.gameObject);
                    curTrans = parent;

                } while (curTrans!=null);
                mInstance = null;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        protected virtual void OnDestroy()
        {
            mInstance = null;
        }
    }
}
