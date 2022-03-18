#if ILRuntime
using UnityEditor;
using UnityEngine;

namespace Game.Editor
{
    [System.Reflection.Obfuscation(Exclude = true)]
    public static class ILRuntimeCLRBinding
    {
        [MenuItem("Tools/ILRuntime/通过自动分析热更DLL生成CLR绑定")]
        public static void GenerateCLRBindingByAnalysis()
        {
            //用新的分析热更dll调用引用来生成绑定代码
            ILRuntime.Runtime.Enviorment.AppDomain domain = new ILRuntime.Runtime.Enviorment.AppDomain();
            foreach (var dllName in HotfixConfig.DllNames)
            {
                using System.IO.FileStream fs = new System.IO.FileStream(AssetUtility.GetHotfixDllAsset(dllName), System.IO.FileMode.Open, System.IO.FileAccess.Read);
                domain.LoadAssembly(fs);
                ILRuntimeUtility.RegisterCrossBindingAdaptor(domain);
                ILRuntime.Runtime.CLRBinding.BindingCodeGenerator.GenerateBindingCode(domain, "Assets/Scripts/ILRuntime/Runtime/Generated");
            }
            
            AssetDatabase.Refresh();
            Debug.Log("生成CLR绑定文件完成");
        }
    }
}
#endif
