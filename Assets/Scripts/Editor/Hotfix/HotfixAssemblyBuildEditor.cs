using System.IO;
using GameFramework;
using UnityEditor;

namespace Game.Editor.Hotfix
{
    [InitializeOnLoad]
    public static class HotfixAssemblyBuildEditor
    {
        /// <summary>
        /// 最原始的程序集路径
        /// </summary>
        private static readonly string s_OriginDllPath = "Library/ScriptAssemblies/";

        private static string GetOriginDllFullPath(string fileName)
        {
            return Utility.Text.Format("{0}{1}", s_OriginDllPath, fileName);
        }

        static HotfixAssemblyBuildEditor()
        {
            for (int i = 0; i < HotfixConfig.DllNames.Length; i++)
            {
                string dllName = HotfixConfig.DllNames[i];
                string dllOriPath = GetOriginDllFullPath(Utility.Text.Format("{0}.dll", dllName));
                string dllDesPath = AssetUtility.GetHotfixDllAsset(dllName);
                string pdbOriPath = GetOriginDllFullPath(Utility.Text.Format("{0}.pdb", dllName));
                string pdbDesPath = AssetUtility.GetHotfixPdbAsset(dllName);

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