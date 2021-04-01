using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MFramework.ServiceLocator.Default {
    /// <summary>
    /// 通用模块生成器
    /// </summary>
    public class AssemblyModuleFactory : IModuleFactory {

        private List<Type> mConcreteTypeCache;

        /// <summary>
        /// 抽象类型与具体类型对应的字典
        /// </summary>
        private Dictionary<Type, Type> mAbstractToConcrete = new Dictionary<Type, Type>();

        public AssemblyModuleFactory(Assembly assembly,Type baseModuleType) {
            //具体类型
            mConcreteTypeCache = assembly
                .GetTypes()
                .Where(t => baseModuleType.IsAssignableFrom(t) && !t.IsAbstract)
                .ToList();

            //具体类型的父接口类型
            foreach (var type in mConcreteTypeCache) {
                var interfaces = type.GetInterfaces();
                foreach (var inter in interfaces) {
                    if(baseModuleType.IsAssignableFrom(inter) && inter != baseModuleType) {
                        mAbstractToConcrete.Add(inter, type);
                    }
                }
            }
        }

        public object CreateModule(ModuleSearchKeys keys) {
            if(keys.Type.IsAbstract) {
                if(mAbstractToConcrete.ContainsKey(keys.Type)) {
                    return mAbstractToConcrete[keys.Type].GetConstructors().First().Invoke(null);
                }
            } else {
                if(mConcreteTypeCache.Contains(keys.Type)) {
                    return keys.Type.GetConstructors().First().Invoke(null);
                }
            }
            return null;
        }

        public object CreateAllModules() {
            return mConcreteTypeCache.Select(t => t.GetConstructors().First().Invoke(null));
        }

    }
}
