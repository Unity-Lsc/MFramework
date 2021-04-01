using UnityEngine;

namespace MFramework.ServiceLocator.ModuleManagerExample {

    public interface IEventManager : IModule {
        void DoSomething();
    }

    public class EventManager : IEventManager {

        private IPoolManager mPoolManager { get; set; }

        public void DoSomething() {
            Debug.Log("EventManager DoSomething...");
        }

        public void InitModule() {
            mPoolManager = ModuleManagementConfig.Container.GetModule<IPoolManager>();
        }
    }
}
