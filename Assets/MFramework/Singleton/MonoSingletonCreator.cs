using System.Linq;
using UnityEngine;

namespace MFramework {

    public static class MonoSingletonCreator {
        public static T CreateMonoSingleton<T>() where T : MonoBehaviour,ISingleton {
            //尝试获取场景中的T脚本
            var instance = Object.FindObjectOfType<T>();
            //如果存在则直接返回
            if(instance) {
                instance.OnSingletonInit();
                return instance;
            }

            //尝试根据MonoSingletonPath去创建单例
            var info = typeof(T);
            instance = info.GetCustomAttributes(false)
                .Cast<MonoSingletonPath>()
                .Select(monoSingletonPath => CreateSingletonWithPath<T>(monoSingletonPath.PathInHierarchy, true))
                .FirstOrDefault();

            //创建实例
            if(!instance) {
                var gameObj = new GameObject(typeof(T).Name);
                Object.DontDestroyOnLoad(gameObj);
                instance = gameObj.AddComponent<T>();
            }
            instance.OnSingletonInit();
            return instance;
        }

        /// <summary>
        /// 根据path创建Singleton
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="dontDestroy"></param>
        /// <returns></returns>
        private static T CreateSingletonWithPath<T>(string path,bool dontDestroy) where T : MonoBehaviour {
            var gameObj = GetOrCreateGameObjectWithPath(path, true, dontDestroy);
            if(!gameObj) {
                gameObj = new GameObject("Singleton of " + typeof(T));
                if(dontDestroy) {
                    Object.DontDestroyOnLoad(gameObj);
                }
            }
            return gameObj.AddComponent<T>();
        }

        private static GameObject GetOrCreateGameObjectWithPath(string path,bool isBuild,bool dontDestroy) {
            if (string.IsNullOrEmpty(path))
                return null;
            var subPath = path.Split('/');
            if (subPath.Length == 0)
                return null;
            return GetOrCreateGameObjectWithpathArray(null, subPath, 0, isBuild, dontDestroy);

        }

        /// <summary>
        /// 递归找到子GameObject节点
        /// </summary>
        /// <param name="parentObj">父物体</param>
        /// <param name="paths">路径集合</param>
        /// <param name="index"></param>
        /// <param name="isBuild"></param>
        /// <param name="dontDestroy"></param>
        /// <returns></returns>
        private static GameObject GetOrCreateGameObjectWithpathArray(GameObject parentObj,string[] paths,int index,bool isBuild,bool dontDestroy) {
            while (true) {
                GameObject curGameObj = null;
                if(!parentObj) {
                    curGameObj = GameObject.Find(paths[index]);
                } else {
                    var child = parentObj.transform.Find(paths[index]);
                    if(child != null) {
                        curGameObj = child.gameObject;
                    }
                }

                if(!curGameObj) {
                    if(isBuild) {
                        curGameObj = new GameObject(paths[index]);
                        if(parentObj != null) {
                            curGameObj.transform.SetParent(parentObj.transform);
                        }
                        if(dontDestroy && index == 0) {
                            Object.DontDestroyOnLoad(curGameObj);
                        }
                    }
                }

                if (!curGameObj) return null;

                //如果是叶子节点直接返回
                if (++index == paths.Length) return curGameObj;
                parentObj = curGameObj;
            }
        }

    }

}
