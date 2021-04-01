using MFramework.ServiceLocator.Default;
using UnityEditor;
using UnityEngine;

namespace MFramework {
    /// <summary>
    /// 编辑器模块化结构
    /// </summary>
    public class EditorModulizationPlatformEditor : EditorWindow {

        /// <summary>
        /// 用来缓存模块的容器
        /// </summary>
        private ModuleContainer mModuleContainer = null;
        
        /// <summary>
        /// 打开窗口
        /// </summary>
        [MenuItem("MFramework/0.EditorModulizationPlatform")]
        public static void Open()
        {
            var editorPlatform = GetWindow<EditorModulizationPlatformEditor>();

            editorPlatform.position = new Rect(
                Screen.width / 2,
                Screen.height * 2 / 3,
                600,
                500
            );
            
            //设置Title文本
            editorPlatform.titleContent = new GUIContent() {
                text = "FrameworkDes"
            };

            var moduleType = typeof(IEditorPlatformModule);
            var cache = new DefaultModuleCache();
            var factory = new AssemblyModuleFactory(moduleType.Assembly, moduleType);

            editorPlatform.mModuleContainer = new ModuleContainer(cache, factory);
            
            editorPlatform.Show();

        }

        private void OnGUI() {
            
            //获取全部模块
            var modules = mModuleContainer.GetAllModules<IEditorPlatformModule>();
            //渲染
            foreach (var editorPlatformModule in modules)
            {
                editorPlatformModule.OnGUI();
            }
        }
    }
}