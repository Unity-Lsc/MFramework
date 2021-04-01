using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace MFramework.ServiceLocator.Pattern.Example {
    public class Example : MonoBehaviour {

        /// <summary>
        /// 自定义的InitialContext
        /// </summary>
        public class InitialContext : AbstractInitialContext {

            public override IService LookUp(string serviceName) {
                ////获取Example所在的Service
                //var assembly = typeof(Example).Assembly;
                //var serviceType = typeof(IService);
                //var service = assembly
                //    .GetTypes()
                //    //获取所有实现IService接口的类型
                //    .Where(t => serviceType.IsAssignableFrom(t) && !t.IsAbstract)
                //    //创建实例
                //    .Select(t => t.GetConstructors().First<ConstructorInfo>().Invoke(null))
                //    //转型为IService
                //    .Cast<IService>()
                //    //获取符合条件的Service对象
                //    .SingleOrDefault(s => s.Name == serviceName);
                //return service;
                IService service = null;
                if(serviceName == "bluetooth") {
                    service = new BluetoothService();
                }
                return service;

            }

        }

        public class BluetoothService : IService {
            public string Name {
                get {
                    return "bluetooth";
                }
            }

            public void Execute() {
                Debug.Log("蓝牙服务启动");
            }
        }

        private void Start() {
            //创建查找器
            var context = new InitialContext();
            //创建服务定位器
            var serviceLacator = new ServiceLocator(context);
            //获取蓝牙服务
            var bluetoothService = serviceLacator.GetService("bluetooth");
            //执行服务
            bluetoothService.Execute();
        }

    }
}
