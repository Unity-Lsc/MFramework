using UnityEngine;

namespace MFramework.ServiceLocator.LayerdArchitectureExample {

    public interface ILoginController : ILogicController {
        void Login();
    }

    /// <summary>
    /// 登录控制器
    /// </summary>
    public class LoginController : ILoginController {

        public void Login() {
            var accountSystem = ArchitectureConfig.Architecture.BusinessModuleLayer.GetModule<IAccountSystem>();
            accountSystem.Login("111", "m111", (succeed) => {
                if (succeed) {
                    Debug.Log("登录成功...");
                }
            });
            
        }

    }

}
