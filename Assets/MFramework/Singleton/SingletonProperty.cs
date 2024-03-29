﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFramework {

    public static class SingletonProperty<T> where T : class,ISingleton {

        private static T mInstance;
        private static readonly object mLock = new object();
        public static T Instance {
            get {
                lock(mLock) {
                    if(mInstance == null) {
                        mInstance = SingletonCreator.CreateSingleton<T>();
                    }
                }
                return mInstance;
            }
        }

        public static void Dispose() {
            mInstance = null;
        }

    }

}
