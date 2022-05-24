using System.Collections.Generic;
using System.IO;
using GameFramework;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Game.Editor.Hotfix
{
    [InitializeOnLoad]
    public static class HotfixAssemblyBuildEditor
    {
        /// <summary>
        /// 最原始的程序集路径
        /// </summary>
        private static readonly string s_OriginDllPath = "Library/ScriptAssemblies/";

        private static readonly string[] s_HotfixReloadScriptPaths = new string[]
        {
            "",
            "Assets/HotfixScripts/LogicView/",
        };

        private static string GetOriginDllFullPath(string fileName)
        {
            return Utility.Text.Format("{0}{1}", s_OriginDllPath, fileName);
        }

        static HotfixAssemblyBuildEditor()
        {
            foreach (var dllName in HotfixConfig.DllNames)
            {
                SyncHotfixDllPdb(dllName);
            }
        }

        private static void SyncHotfixDllPdb(string dllName)
        {
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
            AssetDatabase.Refresh();
        }

        [MenuItem("Tools/Hotfix/Reload", false, 30)]
        public static void HotfixReload()
        {
            BuildHotfixAssembly("Hotfix.Logic", GetScriptPaths("Assets/HotfixScripts/Logic/"));
            BuildHotfixAssembly("Hotfix.LogicView", GetScriptPaths("Assets/HotfixScripts/LogicView/"));
        }

        private static string[] GetScriptPaths(string path)
        {
            List<string> scripts = new List<string>();
            DirectoryInfo dti = new DirectoryInfo(path);
            FileInfo[] fileInfos = dti.GetFiles("*.cs", SearchOption.AllDirectories);
            foreach (var t in fileInfos)
            {
                scripts.Add(t.FullName);
            }

            return scripts.ToArray();
        }

        private static void BuildHotfixAssembly(string dllName, string[] scripts)
        {
            string dllFullPath = GetOriginDllFullPath(dllName);
            if (File.Exists(dllFullPath))
            {
                File.Delete(dllFullPath);
            }
            AssemblyBuilder assemblyBuilder = new AssemblyBuilder(dllFullPath, scripts);
            assemblyBuilder.buildFinished += (s, messages) =>
            {
                foreach (CompilerMessage message in messages)
                {
                    if (message.type == CompilerMessageType.Error)
                    {
                        Debug.LogError(message.message);
                    }
                    else if (message.type == CompilerMessageType.Warning)
                    {
                        Debug.LogWarning(message.message);
                    }
                    else if (message.type == CompilerMessageType.Info)
                    {
                        Debug.Log(message.message);
                    }
                }
            };
            if (assemblyBuilder.Build())
            {
                SyncHotfixDllPdb(dllName);
            }
            else
            {
                Debug.LogErrorFormat("Reload {0} Error!", dllName);
            }
        }
    }
}