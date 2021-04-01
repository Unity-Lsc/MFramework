using System;
using System.Reflection;

namespace MFramework {

    /// <summary>
    /// 通过反射,创建一个ISingleton的实例
    /// </summary>
    public static class SingletonCreator {

        public static T CreateSingleton<T>() where T : class,ISingleton {
            //获取私有构造函数
            var ctors = typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);
            //获取无参构造函数
            var ctor = Array.Find(ctors, c => c.GetParameters().Length == 0);
            if(ctor == null) {
                throw new Exception("Non-public Constructor() not found! in " + typeof(T));
            }
            //通过构造函数,创建实例
            var retInstance = ctor.Invoke(null) as T;
            retInstance.OnSingletonInit();
            return retInstance;
        }

    }

}
