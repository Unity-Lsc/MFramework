using MFramework.ServiceLocator.Default;

namespace MFramework.ServiceLocator.LayerdArchitectureExample {

    public interface IPublicModuleController { }

    /// <summary>
    /// 公共模块层(UIManager ResManager等)
    /// </summary>
    public class PublicModuleLayer : AbstractModuleLayer, IPublicModuleLayer {

        protected override AssemblyModuleFactory mFactory {
            get {
                return new AssemblyModuleFactory(typeof(IPublicModuleController).Assembly, typeof(IPublicModuleController));
            }
        }
    }
}
