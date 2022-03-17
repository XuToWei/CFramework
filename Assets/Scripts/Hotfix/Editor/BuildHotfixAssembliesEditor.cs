using System.IO;
using UnityEditor;

namespace Game.Editor
{
    [InitializeOnLoad]
    public static class BuildHotfixAssembliesEditor
    {
        /// <summary>
        /// 最原始的4个程序集路径
        /// </summary>
        private static readonly string[] s_OriginDllDirs = new[]
        {
            "Library/ScriptAssemblies/Hotfix",
        };

        /// <summary>
        /// 最原始的4个程序集对应名称
        /// </summary>
        private static readonly string[] s_OriginDllName = new[]
        {
            "Hotfix",
        };

        /// <summary>
        /// 最终的Hotfix dll路径
        /// </summary>
        private static string s_FinalHotfixDllDir = "Assets/Res/Codes/";

        static BuildHotfixAssembliesEditor()
        {
            // for (int i = 0; i < s_OriginDllDirs.Length; i++)
            // {
            //     string dllOriPath = s_OriginDllDirs[i] + ".dll";
            //     string dllDesPath = Path.Combine(s_FinalHotfixDllDir, s_OriginDllName[i] + ".dll.bytes");
            //
            //     string pdbOriPath = s_OriginDllDirs[i] + ".pdb";
            //     string pdbDesPath = Path.Combine(s_FinalHotfixDllDir, s_OriginDllName[i] + ".pdb.bytes");
            //
            //     File.Copy(dllOriPath, dllDesPath, true);
            //     File.Copy(pdbOriPath, pdbDesPath, true);
            //     AssetDatabase.ImportAsset(dllDesPath);
            //     AssetDatabase.ImportAsset(pdbDesPath);
            // }
            //
            // AssetDatabase.Refresh();
        }
    }
}