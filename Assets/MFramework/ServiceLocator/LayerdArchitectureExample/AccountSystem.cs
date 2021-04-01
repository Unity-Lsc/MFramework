using System;
using UnityEngine;

namespace MFramework.ServiceLocator.LayerdArchitectureExample {

    public interface IAccountSystem : ISystem {
        bool IsLogin { get; }
        void Login(string userName, string password, Action<bool> onLogin);
        void Logout(Action onLogout);
    }

    /// <summary>
    /// 账户业务
    /// </summary>
    public class AccountSystem : IAccountSystem {

        public bool IsLogin { get; private set; }

        public void Login(string userName, string password, Action<bool> onLogin) {
            PlayerPrefs.SetString("userName", userName);
            PlayerPrefs.SetString("password", password);
            IsLogin = true;
            onLogin(IsLogin);
        }

        public void Logout(Action onLogout) {
            PlayerPrefs.SetString("userName", string.Empty);
            PlayerPrefs.SetString("password", string.Empty);
            IsLogin = false;
        }
    }

}
