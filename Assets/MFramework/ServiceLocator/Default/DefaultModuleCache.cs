using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFramework.ServiceLocator.Default {
    /// <summary>
    /// 通用模块缓存器
    /// </summary>
    public class DefaultModuleCache : IModuleCache {

        private Dictionary<Type, List<object>> mModulesByType = new Dictionary<Type, List<object>>();

        public void AddModule(ModuleSearchKeys keys, object module) {
            if (mModulesByType.ContainsKey(keys.Type)) {
                mModulesByType[keys.Type].Add(module);
            } else {
                mModulesByType.Add(keys.Type, new List<object>() { module });
            }
        }
        
        public object GetModule(ModuleSearchKeys keys) {
            List<object> modules = null;
            if (mModulesByType.TryGetValue(keys.Type,out modules)) {
                return modules.FirstOrDefault();
            }
            return null;
        }

        public object GetAllModules() {
            //SelectMany可以理解为二维遍历
            return mModulesByType.Values.SelectMany(list => list);
        }
    }
}
