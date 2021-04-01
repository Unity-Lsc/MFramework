using UnityEngine;

namespace MFramework.ServiceLocator.ModuleManagerExample {

    public interface IResManager : IModule {
        void DoSomething();
    }

    public class ResManager : IResManager {

        private IPoolManager mPoolManager { get; set; }

        public void DoSomething() {
            Debug.Log("ResManager DoSomething...");
        }

        public void InitModule() {
            mPoolManager = ModuleManagementConfig.Container.GetModule<IPoolManager>();
        }
    }
}
