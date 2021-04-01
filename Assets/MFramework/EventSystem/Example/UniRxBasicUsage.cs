using UniRx;
using UnityEngine;

namespace MFramework.Example {

    public class UniRxBasicUsage : MonoBehaviour {

        private void Start() {
            Observable.EveryUpdate()
                .Where(_ => Time.frameCount % 5 == 0)
                .Subscribe(_ => Debug.Log("每5帧打印一个hello..."));
        }

    }

}