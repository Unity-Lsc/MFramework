using UnityEngine;

namespace MFramework.ServiceLocator.LayerdArchitectureExample {

    public interface IMissionSystem : ISystem {
        void OnEvent(string eventName);
    }

    /// <summary>
    /// 任务业务
    /// </summary>
    public class MissionSystem : IMissionSystem {

        private int mJumpCount {
            get { return PlayerPrefs.GetInt("JUMP_COUNT"); }
            set { PlayerPrefs.SetInt("JUMP_COUNT", value); }
        }

        public void OnEvent(string eventName) {
            if(eventName == "JUMP") {
                mJumpCount++;
                if(mJumpCount == 1) {
                    Debug.Log("第一次跳跃,任务完成...");
                }else if(mJumpCount == 5) {
                    Debug.Log("跳跃新手,任务完成...");
                }else if(mJumpCount == 10) {
                    Debug.Log("跳跃达人,任务完成...");
                }
            }
        }

    }
}
