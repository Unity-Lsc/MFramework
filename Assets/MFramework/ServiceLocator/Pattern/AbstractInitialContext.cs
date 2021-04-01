
namespace MFramework.ServiceLocator.Pattern {
    /// <summary>
    /// 服务查找抽象类
    /// </summary>
    public abstract class AbstractInitialContext {

        public abstract IService LookUp(string serviceName);

    }

}