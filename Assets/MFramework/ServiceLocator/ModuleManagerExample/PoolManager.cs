using UnityEngine;

namespace MFramework.ServiceLocator.ModuleManagerExample {

    public interface IPoolManager : IModule {
        void DoSomething();
    }

    public class PoolManager : IPoolManager {

        public void DoSomething() {
            Debug.Log("PoolManager DoSomething...");
        }

        public void InitModule() {
            
        }
    }
}
