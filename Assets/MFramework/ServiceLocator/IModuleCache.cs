using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFramework.ServiceLocator {
    /// <summary>
    /// 模块缓存接口
    /// </summary>
    public interface IModuleCache {

        object GetModule(ModuleSearchKeys keys);

        object GetAllModules();

        void AddModule(ModuleSearchKeys keys, object module);

    }

}
