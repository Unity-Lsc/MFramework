using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFramework.ServiceLocator {
    /// <summary>
    /// 模块工厂接口
    /// </summary>
    public interface IModuleFactory {

        object CreateModule(ModuleSearchKeys keys);

        object CreateAllModules();

    }
}
