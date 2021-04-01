
namespace MFramework.ServiceLocator.LayerdArchitectureExample {
    /// <summary>
    /// 层级的公共接口
    /// </summary>
    public interface IModuleLayer {

        T GetModule<T>() where T : class;

    }

    /// <summary>
    /// 逻辑层 UI/Game
    /// </summary>
    public interface ILogicLayer : IModuleLayer {

    }

    /// <summary>
    /// 业务模块层
    /// </summary>
    public interface IBusinessModuleLayer : IModuleLayer {

    }

    /// <summary>
    /// 公共模块层
    /// </summary>
    public interface IPublicModuleLayer : IModuleLayer {

    }

    /// <summary>
    /// 基础设施层
    /// </summary>
    public interface IBasicModuleLayer : IModuleLayer {

    }

    /// <summary>
    /// 工具层
    /// </summary>
    public interface IUtilityLayer : IModuleLayer {

    }

}
