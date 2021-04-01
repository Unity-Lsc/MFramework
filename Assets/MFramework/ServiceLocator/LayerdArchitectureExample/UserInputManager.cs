using UnityEngine;

namespace MFramework.ServiceLocator.LayerdArchitectureExample {

    public interface IUserInputManager : ILogicController {
        void OnInput(KeyCode keyCode);
    }

    /// <summary>
    /// 用户输入管理
    /// </summary>
    public class UserInputManager : IUserInputManager {

        public void OnInput(KeyCode keyCode) {
            Debug.Log("输入了:" + keyCode);
            var missionSystem = ArchitectureConfig.Architecture.BusinessModuleLayer.GetModule<IMissionSystem>();
            missionSystem.OnEvent("JUMP");
        }
    }

}
