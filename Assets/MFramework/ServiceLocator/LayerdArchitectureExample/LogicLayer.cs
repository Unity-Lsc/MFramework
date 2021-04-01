using MFramework.ServiceLocator.Default;

namespace MFramework.ServiceLocator.LayerdArchitectureExample {

    public interface ILogicController { }

    /// <summary>
    /// 逻辑层
    /// </summary>
    public class LogicLayer : AbstractModuleLayer, ILogicLayer {

        protected override AssemblyModuleFactory mFactory {
            get {
                return new AssemblyModuleFactory(typeof(ILogicController).Assembly, typeof(ILogicController));
            }
        }

    }
}
