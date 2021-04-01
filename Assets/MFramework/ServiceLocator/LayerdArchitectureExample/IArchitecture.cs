using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFramework.ServiceLocator.LayerdArchitectureExample {

    public interface IArchitecture {

        ILogicLayer LogicLayer { get; }

        IBusinessModuleLayer BusinessModuleLayer { get; }

        IPublicModuleLayer PublicModuleLayer { get; }

        IUtilityLayer UtilityLayer { get; }

        IBasicModuleLayer BasicModuleLayer { get; }

    }

}
