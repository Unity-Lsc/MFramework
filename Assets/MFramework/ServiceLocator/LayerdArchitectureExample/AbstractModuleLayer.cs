using MFramework.ServiceLocator.Default;

namespace MFramework.ServiceLocator.LayerdArchitectureExample {

    public abstract class AbstractModuleLayer : IModuleLayer {

        private ModuleContainer mContainer = null;

        //在子类中提供
        protected abstract AssemblyModuleFactory mFactory { get; }

        public AbstractModuleLayer() {
            mContainer = new ModuleContainer(new DefaultModuleCache(), mFactory);
        }

        public T GetModule<T>() where T : class {
            return mContainer.GetModule<T>();
        }
    }

}
