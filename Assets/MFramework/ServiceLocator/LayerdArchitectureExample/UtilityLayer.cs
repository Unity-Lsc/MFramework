using MFramework.ServiceLocator.Default;

namespace MFramework.ServiceLocator.LayerdArchitectureExample {

    public interface IUtiletyLayerController { }

    /// <summary>
    /// 工具层(工具类或者工具方法 如FSM,Json序列化帮助器等,或者是网络请求层)
    /// </summary>
    public class UtilityLayer : AbstractModuleLayer, IUtilityLayer {

        protected override AssemblyModuleFactory mFactory {
            get {
                return new AssemblyModuleFactory(typeof(IUtiletyLayerController).Assembly, typeof(IUtiletyLayerController));
            }
        }
    }
}
