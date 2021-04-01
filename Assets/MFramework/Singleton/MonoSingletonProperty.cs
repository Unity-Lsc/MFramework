using UnityEngine;

namespace MFramework {

    public static class MonoSingletonProperty<T> where T : MonoBehaviour,ISingleton {

        private static T mInstance;
        public static T Instance {
            get {
                if(mInstance == null) {
                    mInstance = MonoSingletonCreator.CreateMonoSingleton<T>();
                }
                return mInstance;
            }
        }

        public static void Dispose() {
            Object.Destroy(mInstance.gameObject);
            mInstance = null;
        }

    }

}
