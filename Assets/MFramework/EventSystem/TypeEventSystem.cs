using System;
using System.Collections.Generic;

namespace MFramework {

    public class TypeEventSystem {

        /// <summary>
        /// 接口 只负责存储在字典中
        /// </summary>
        interface IRegisterations {

        }

        class Registerations<T> : IRegisterations {
            public Action<T> OnReceive = obj => { };
        }

        private static Dictionary<Type, IRegisterations> mTypeEventDict = new Dictionary<Type, IRegisterations>();

        /// <summary>
        /// 注册事件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="onReceive"></param>
        public static void Register<T>(Action<T> onReceive) {
            var type = typeof(T);
            IRegisterations registerations = null;
            //已经有同一个类型的注册 只需要再增加委托即可
            if(mTypeEventDict.TryGetValue(type, out registerations)) {
                var reg = registerations as Registerations<T>;
                reg.OnReceive += onReceive;
            } else {
                var reg = new Registerations<T>();
                reg.OnReceive += onReceive;
                mTypeEventDict.Add(type, reg);
            }
        }

        /// <summary>
        /// 注销事件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="onReceive"></param>
        public static void UnRegister<T>(Action<T> onReceive) {
            var type = typeof(T);
            IRegisterations registerations = null;
            if(mTypeEventDict.TryGetValue(type,out registerations)) {
                var reg = registerations as Registerations<T>;
                reg.OnReceive -= onReceive;
            }
        }

        /// <summary>
        /// 发送事件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        public static void Send<T>(T t) {
            var type = typeof(T);
            IRegisterations registerations = null;
            if(mTypeEventDict.TryGetValue(type,out registerations)) {
                var reg = registerations as Registerations<T>;
                reg.OnReceive(t);
            }
        }

    }

}