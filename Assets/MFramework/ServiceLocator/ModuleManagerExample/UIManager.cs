using UnityEngine;

namespace MFramework.ServiceLocator.ModuleManagerExample {

    public interface IUIManager : IModule {
        void DoSomething();
    }

    public class UIManager : IUIManager {

        private IResManager mResManager { get; set; }
        private IEventManager mEventManager { get; set; }

        public void InitModule() {
            mResManager = ModuleManagementConfig.Container.GetModule<IResManager>();
            mEventManager = ModuleManagementConfig.Container.GetModule<IEventManager>();
        }

        public void DoSomething() {
            Debug.Log("UIManager DoSomething...");
            mResManager.DoSomething();
            mEventManager.DoSomething();
        }
        
    }
}
