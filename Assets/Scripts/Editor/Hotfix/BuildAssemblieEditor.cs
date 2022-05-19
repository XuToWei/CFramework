using System.IO;
using UnityEditor;

namespace Game.Editor.Hotfix
{
    [InitializeOnLoad]
    public static class HotfixAssemblyBuildEditor
    {
        /// <summary>
        /// 最原始的4个程序集路径
        /// </summary>
        private static string[] s_OriginDllDirs = new[]
        {
            "Library/ScriptAssemblies/Hotfix.Model",
            "Library/ScriptAssemblies/Hotfix.ModelView",
            "Library/ScriptAssemblies/Hotfix.Hotfix",
            "Library/ScriptAssemblies/Hotfix.HotfixView"
        };

        /// <summary>
        /// 最原始的4个程序集对应名称
        /// </summary>
        private static string[] s_OriginDllName = new[]
        {
            "Model",
            "ModelView",
            "Hotfix",
            "HotfixView"
        };

        /// <summary>
        /// 最终的Hotfix dll路径
        /// </summary>
        private static string s_FinalHotfixDllDir = "Assets/Res/HotfixDlls/";

        static HotfixAssemblyBuildEditor()
        {
            for (int i = 0; i < s_OriginDllDirs.Length; i++)
            {
                string dllOriPath = s_OriginDllDirs[i] + ".dll";
                string dllDesPath = Path.Combine(s_FinalHotfixDllDir, s_OriginDllName[i] + ".dll.bytes");
            
                string pdbOriPath = s_OriginDllDirs[i] + ".pdb";
                string pdbDesPath = Path.Combine(s_FinalHotfixDllDir, s_OriginDllName[i] + ".pdb.bytes");

                if (File.Exists(dllOriPath))
                {
                    File.Copy(dllOriPath, dllDesPath, true);
                    AssetDatabase.ImportAsset(dllDesPath);
                }
                if (File.Exists(pdbOriPath))
                {
                    File.Copy(pdbOriPath, pdbDesPath, true);
                    AssetDatabase.ImportAsset(pdbDesPath);
                }
            }
            AssetDatabase.Refresh();
        }
    }
}