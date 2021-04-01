
using MFramework.ServiceLocator.Default;

namespace MFramework.ServiceLocator.LayerdArchitectureExample {

    public interface ISystem { }

    /// <summary>
    /// 业务模块层
    /// </summary>
    public class BusinessModuleLayer : AbstractModuleLayer, IBusinessModuleLayer {

        protected override AssemblyModuleFactory mFactory {
            get {
                return new AssemblyModuleFactory(typeof(ISystem).Assembly, typeof(ISystem));
            }
        }
    }

}
