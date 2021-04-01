using System;
using UnityEngine;
using UniRx;

namespace FrameworkDesign2021.Example {

    public class UniRxTypeEventSystemTest : MonoBehaviour {
        class A {
        }

        class B {
            public int Age;
            public string Name;
        }

        IDisposable mEventADisposable;

        private void Start() {

            mEventADisposable = UniRxTypeEventSystem.GetEvent<A>()
                .Subscribe(ReceiveA);
            UniRxTypeEventSystem.GetEvent<B>()
                .Subscribe(ReceiveB)
                .AddTo(this);

        }

        void ReceiveA(A a) {
            Debug.Log("received A");
        }

        void ReceiveB(B b) {
            Debug.LogFormat("received B:{0} {1}", b.Name, b.Age);
        }

        private void Update() {
            if (Input.GetMouseButtonDown(0)) {
                UniRxTypeEventSystem.Send(new A());
            }

            if (Input.GetMouseButtonDown(1)) {
                UniRxTypeEventSystem.Send(new B() {
                    Age = 10,
                    Name = "LSC"
                });
            }

            if (Input.GetKeyDown(KeyCode.U)) {
                mEventADisposable.Dispose();
            }
        }
    }

}