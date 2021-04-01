using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MFramework.Example {

    public class SingletonExample : MonoBehaviour {

        private void Start() {
            GameManager.Instance.PlayGame();
        }

        public class GameManager : Singleton<GameManager> {
            
            private GameManager() { }

            public void PlayGame() {
                Debug.Log("PlayGame...");
            }

        }

    }

}