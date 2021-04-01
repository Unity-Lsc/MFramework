using UnityEngine;

namespace MFramework {

    public abstract class MonoSingleton<T> : MonoBehaviour, ISingleton where T : MonoSingleton<T> {

        protected static T mInstance;
        public static T Instance {
            get {
                if(mInstance == null && !mOnApplicationQuit) {
                    mInstance = MonoSingletonCreator.CreateMonoSingleton<T>();
                }
                return mInstance;
            }
        }

        public virtual void OnSingletonInit() {
            
        }

        protected static bool mOnApplicationQuit = false;

        public static bool IsApplicationQuit {
            get {
                return mOnApplicationQuit;
            }
        }

        protected virtual void OnApplicationQuit() {
            mOnApplicationQuit = true;
            if (mInstance == null) return;
            Destroy(mInstance.gameObject);
            mInstance = null;
        }

        public virtual void Dispose() {
            Destroy(gameObject);
        }

        protected virtual void OnDestroy() {
            mInstance = null;
        }

    }

}
