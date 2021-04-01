using MFramework.ServiceLocator;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MFramework
{
    public class ModuleContainer {
        
        private IModuleCache mCache;
        private IModuleFactory mFactory;

        public ModuleContainer(IModuleCache cache,IModuleFactory factory) {
            mCache = cache;
            mFactory = factory;
        }

        public T GetModule<T>() where T : class {
            //申请对象
            var moduleSearchKeys = ModuleSearchKeys.Allocate<T>();
            var module = mCache.GetModule(moduleSearchKeys);
            if(module == null) {
                module = mFactory.CreateModule(moduleSearchKeys);
                mCache.AddModule(moduleSearchKeys, module);
            }

            //回收对象
            moduleSearchKeys.Release2Pool();

            return module as T;
        }

        public IEnumerable<T> GetAllModules<T>() where T : class {
            //申请对象
            var moduleSearchKeys = ModuleSearchKeys.Allocate<T>();
            var modules = mCache.GetAllModules() as IEnumerable<object>;
            if(modules == null || !modules.Any()) {
                modules = mFactory.CreateAllModules() as IEnumerable<object>;
                foreach (var module in modules) {
                    mCache.AddModule(moduleSearchKeys, module);
                }
            }

            //回收对象
            moduleSearchKeys.Release2Pool();

            return modules.Select(m => m as T);
        }
        
    }
}