using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MFramework.Example {

    public class MasterBehaviourExample : MonoBehaviour {

        public class EventA { }
        public class EventB { }

        EventService mEventService = new EventService();

        private void Start() {
            mEventService.Register<EventA>(OnEventARegister);

            mEventService.Register<EventB>((EventB) => {
                Debug.Log("On Event B Receive...");
            });

        }

        void OnEventARegister(EventA eventA) {
            Debug.Log("On Event A Receive...");
        }

        private void Update() {
            if(Input.GetMouseButtonDown(0)) {
                mEventService.Send(new EventA());
                mEventService.Send(new EventB());
            }
        }

        private void OnDestroy() {
            mEventService.UnRegisterAll();
            mEventService = null;
        }

    }

}