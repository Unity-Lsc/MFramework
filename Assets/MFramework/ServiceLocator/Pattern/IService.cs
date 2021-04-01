
namespace MFramework.ServiceLocator.Pattern {
    /// <summary>
    /// 服务接口
    /// </summary>
    public interface IService {
        string Name { get; }
        void Execute();
    }

}