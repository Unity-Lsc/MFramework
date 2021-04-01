using System;
using UniRx;
using UnityEngine;

namespace MFramework.Example {

    public class UniRxTimerExample : MonoBehaviour {

        private void Start() {

            Debug.Log("此时:" + DateTime.Now);

            Observable.Timer(TimeSpan.FromSeconds(5.0f))
                .Subscribe(_ => Debug.Log("此时:" + DateTime.Now))
                .AddTo(this);

            Observable.Timer(TimeSpan.FromSeconds(3.5f))
                .Subscribe(_ => Destroy(gameObject));

        }

    }

}
