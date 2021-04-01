using System.Collections.Generic;
using System.Linq;

namespace MFramework.ServiceLocator.Pattern {
    /// <summary>
    /// 服务缓存
    /// </summary>
    public class Cache {

        private List<IService> mServices = new List<IService>();

        /// <summary>
        /// 根据服务名,获取服务
        /// </summary>
        /// <param name="serviceName">服务名</param>
        public IService GetService(string serviceName) {
            return mServices.SingleOrDefault(s => s.Name == serviceName);
        }

        /// <summary>
        /// 添加服务
        /// </summary>
        /// <param name="service">要添加的服务</param>
        public void AddService(IService service) {
            mServices.Add(service);
        }

    }

}