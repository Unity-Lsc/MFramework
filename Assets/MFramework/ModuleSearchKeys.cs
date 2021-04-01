using System;
using System.Collections.Generic;

namespace MFramework {
    /// <summary>
    /// 模块搜索关键字
    /// </summary>
    public class ModuleSearchKeys {

        public string Name { get; set; }

        public Type Type { get; set; }

        //私有构造,防止用户自己new
        private ModuleSearchKeys() { }

        //默认为10个容量
        private static Stack<ModuleSearchKeys> mPool = new Stack<ModuleSearchKeys>(10);

        public static ModuleSearchKeys Allocate<T>() {
            ModuleSearchKeys keys = null;
            keys = mPool.Count != 0 ? mPool.Pop() : new ModuleSearchKeys();
            keys.Type = typeof(T);
            return keys;
        }

        /// <summary>
        /// key回收至池子中
        /// </summary>
        public void Release2Pool() {
            Type = null;
            Name = null;
            mPool.Push(this);
        }

    }

}
