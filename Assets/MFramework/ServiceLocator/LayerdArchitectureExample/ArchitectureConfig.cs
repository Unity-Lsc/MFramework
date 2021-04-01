using UnityEngine;

namespace MFramework.ServiceLocator.LayerdArchitectureExample {
    /// <summary>
    /// 全局配置
    /// </summary>
    public class ArchitectureConfig : IArchitecture {

        public ILogicLayer LogicLayer { get; private set; }
        public IBusinessModuleLayer BusinessModuleLayer { get; private set; }
        public IPublicModuleLayer PublicModuleLayer { get; private set; }
        public IUtilityLayer UtilityLayer { get; private set; }
        public IBasicModuleLayer BasicModuleLayer { get; private set; }

        public static ArchitectureConfig Architecture = null;

        /// <summary>
        /// 项目启动时,自动运行
        /// </summary>
        [RuntimeInitializeOnLoadMethod]
        static void Config() {
            Architecture = new ArchitectureConfig();

            //逻辑层配置
            Architecture.LogicLayer = new LogicLayer();

            //主动创建对象
            var loginController = Architecture.LogicLayer.GetModule<ILoginController>();
            var userInputManager = Architecture.LogicLayer.GetModule<IUserInputManager>();

            //业务模块层配置
            Architecture.BusinessModuleLayer = new BusinessModuleLayer();

            var accountSystem = Architecture.BusinessModuleLayer.GetModule<IAccountSystem>();
            var missionSystem = Architecture.BusinessModuleLayer.GetModule<IMissionSystem>();

            //公共模块层配置
            Architecture.PublicModuleLayer = new PublicModuleLayer();


            //工具层配置
            Architecture.UtilityLayer = new UtilityLayer();


            //基础设施层配置
            Architecture.BasicModuleLayer = new BasicModuleLayer();

        }

    }

}
