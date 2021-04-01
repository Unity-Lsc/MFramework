using UnityEngine;

namespace MFramework.Example {

    public class MonoSingletonExample : MonoBehaviour {


        private void Start() {
            var instance1 = GameManager.Instance;
            var instance2 = GameManager.Instance;
            Debug.Log(instance1 == instance2);
        }

        public class GameManager : MonoSingleton<GameManager> {
            public override void OnSingletonInit() {
                base.OnSingletonInit();
                Debug.Log("GameManager Init...");
            }
        }


    }

}
