using UnityEngine;

namespace MFramework.ServiceLocator.ModuleManagerExample {

    public interface IFSM: IModule {
        void DoSomething();
    }

    public class FSM : IFSM {

        public void DoSomething() {
            Debug.Log("FSM DoSomething...");
        }

        public void InitModule() {
            
        }
    }
}
