using UnityEngine;

namespace MFramework.ServiceLocator.LayerdArchitectureExample {

    public class LayerdArchitectureExample : MonoBehaviour {

        private ILoginController mLoginController;
        private IUserInputManager mUserInputManager;

        private void Start() {
            mLoginController = ArchitectureConfig.Architecture.LogicLayer.GetModule<ILoginController>();
            mUserInputManager = ArchitectureConfig.Architecture.LogicLayer.GetModule<IUserInputManager>();

            mLoginController.Login();

        }

        private void Update() {
            if(Input.GetKeyUp(KeyCode.Space)) {
                mUserInputManager.OnInput(KeyCode.Space);
            }
        }

    }

}
