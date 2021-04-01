using System;

namespace MFramework.ServiceLocator.Pattern {
    /// <summary>
    /// 服务定位器(通过此类获取服务)
    /// </summary>
    public class ServiceLocator {

        private readonly Cache mCache = new Cache();

        private readonly AbstractInitialContext mContext;

        public ServiceLocator(AbstractInitialContext context) {
            mContext = context;
        }

        /// <summary>
        /// 根据服务名,获取服务
        /// </summary>
        /// <param name="serviceName">服务名</param>
        public IService GetService(string serviceName) {
            var service = mCache.GetService(serviceName);
            if(service == null) {
                service = mContext.LookUp(serviceName);
                mCache.AddService(service);
            }
            if(service == null) {
                throw new Exception("Service:" + serviceName + " 不存在");
            }
            return service;
        }

    }

}
