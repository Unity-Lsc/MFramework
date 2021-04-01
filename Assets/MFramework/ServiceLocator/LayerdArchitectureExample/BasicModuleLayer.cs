using MFramework.ServiceLocator.Default;

namespace MFramework.ServiceLocator.LayerdArchitectureExample {

    public interface IBasicModuleLayerController { }

    /// <summary>
    /// 基础设施层(网络,持久化,日志等)
    /// </summary>
    public class BasicModuleLayer : AbstractModuleLayer, IBasicModuleLayer {

        protected override AssemblyModuleFactory mFactory {
            get {
                return new AssemblyModuleFactory(typeof(IBasicModuleLayerController).Assembly, typeof(IBasicModuleLayerController));
            }
        }
    }
}
