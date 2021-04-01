using System.Collections.Generic;
using System;
using UniRx;

namespace FrameworkDesign2021.Example {

    public class UniRxTypeEventSystem {
        /// <summary>
        /// 接口 只负责存储在字典中
        /// </summary>
        interface IRegisterations {

        }

        /// <summary>
        /// 多个注册
        /// </summary>
        class Registerations<T> : IRegisterations {
            /// <summary>
            /// 不需要 List<Action<T>> 了
            /// 因为委托本身就可以一对多注册
            /// </summary>
            public Subject<T> Subject = new Subject<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        private static Dictionary<Type, IRegisterations> mTypeEventDict = new Dictionary<Type, IRegisterations>();

        /// <summary>
        /// 注册事件
        /// </summary>
        /// <param name="onReceive"></param>
        /// <typeparam name="T"></typeparam>
        public static Subject<T> GetEvent<T>() {
            var type = typeof(T);

            IRegisterations registerations = null;

            if (mTypeEventDict.TryGetValue(type, out registerations)) {
                var reg = registerations as Registerations<T>;
                return reg.Subject;
            } else {
                var reg = new Registerations<T>();
                mTypeEventDict.Add(type, reg);
                return reg.Subject;
            }
        }

        /// <summary>
        /// 发送事件
        /// </summary>
        /// <param name="t"></param>
        /// <typeparam name="T"></typeparam>
        public static void Send<T>(T t) {
            var type = typeof(T);

            IRegisterations registerations = null;

            if (mTypeEventDict.TryGetValue(type, out registerations)) {
                var reg = registerations as Registerations<T>;
                reg.Subject.OnNext(t);
            }
        }
    }

}