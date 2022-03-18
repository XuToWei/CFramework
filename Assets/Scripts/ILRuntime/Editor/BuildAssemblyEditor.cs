using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using GameFramework;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;

namespace Game.Editor
{
    public static class BuildAssemblyEditor
    {
        private const string CodeDir = "Assets/Bundles/Code/";

        private static readonly string[] AdditionalReferences = new []
        {
            ""
        };

        [MenuItem("Tools/BuildCodeDebug _F5")]
        public static void BuildCodeDebug()
        {
            BuildMuteAssembly("Hotfix", new []
            {
                "Codes/",
            }, Array.Empty<string>(), CodeOptimization.Debug);

            AfterCompiling();
            
            AssetDatabase.Refresh();
        }
        
        [MenuItem("Tools/BuildCodeRelease _F6")]
        public static void BuildCodeRelease()
        {
            BuildMuteAssembly("Hotfix", new []
            {
                "Codes/",
            }, Array.Empty<string>(), CodeOptimization.Release);

            AfterCompiling();
            
            AssetDatabase.Refresh();
        }

        [MenuItem("Tools/BuildLogic _F7")]
        public static void BuildReload()
        {
            if (Application.isPlaying && GameEntry.Base.EditorResourceMode)
            {
                throw new GameFrameworkException("需要在运行时候使用!");
            }
            BuildCodeDebug();
            GameEntry.Hotfix.Load(null);
        }

        private static void BuildMuteAssembly(string assemblyName, string[] CodeDirectorys, string[] additionalReferences, CodeOptimization codeOptimization)
        {
            List<string> scripts = new List<string>();
            for (int i = 0; i < CodeDirectorys.Length; i++)
            {
                DirectoryInfo dti = new DirectoryInfo(CodeDirectorys[i]);
                FileInfo[] fileInfos = dti.GetFiles("*.cs", System.IO.SearchOption.AllDirectories);
                for (int j = 0; j < fileInfos.Length; j++)
                {
                    scripts.Add(fileInfos[j].FullName);
                }
            }

            string dllPath = Path.Combine(HotfixConfig.DllFolderPath, $"{assemblyName}.dll");
            string pdbPath = Path.Combine(HotfixConfig.DllFolderPath, $"{assemblyName}.pdb");
            File.Delete(dllPath);
            File.Delete(pdbPath);

            if (!Directory.Exists(HotfixConfig.DllFolderPath))
            {
                Directory.CreateDirectory(HotfixConfig.DllFolderPath);
            }

            AssemblyBuilder assemblyBuilder = new AssemblyBuilder(dllPath, scripts.ToArray());
            
            //启用UnSafe
            //assemblyBuilder.compilerOptions.AllowUnsafeCode = true;

            BuildTargetGroup buildTargetGroup = BuildPipeline.GetBuildTargetGroup(EditorUserBuildSettings.activeBuildTarget);

            assemblyBuilder.compilerOptions.CodeOptimization = codeOptimization;
            assemblyBuilder.compilerOptions.ApiCompatibilityLevel = PlayerSettings.GetApiCompatibilityLevel(buildTargetGroup);
            // assemblyBuilder.compilerOptions.ApiCompatibilityLevel = ApiCompatibilityLevel.NET_4_6;

            assemblyBuilder.additionalReferences = additionalReferences;
            
            assemblyBuilder.flags = AssemblyBuilderFlags.None;
            //AssemblyBuilderFlags.None                 正常发布
            //AssemblyBuilderFlags.DevelopmentBuild     开发模式打包
            //AssemblyBuilderFlags.EditorAssembly       编辑器状态
            assemblyBuilder.referencesOptions = ReferencesOptions.UseEngineModules;

            assemblyBuilder.buildTarget = EditorUserBuildSettings.activeBuildTarget;

            assemblyBuilder.buildTargetGroup = buildTargetGroup;

            assemblyBuilder.buildStarted += delegate(string assemblyPath) { Debug.LogFormat("build start：" + assemblyPath); };

            assemblyBuilder.buildFinished += delegate(string assemblyPath, CompilerMessage[] compilerMessages)
            {
                int errorCount = compilerMessages.Count(m => m.type == CompilerMessageType.Error);
                int warningCount = compilerMessages.Count(m => m.type == CompilerMessageType.Warning);

                Debug.LogFormat("Warnings: {0} - Errors: {1}", warningCount, errorCount);

                if (warningCount > 0)
                {
                    Debug.LogFormat("有{0}个Warning!!!", warningCount);
                }

                if (errorCount > 0)
                {
                    for (int i = 0; i < compilerMessages.Length; i++)
                    {
                        if (compilerMessages[i].type == CompilerMessageType.Error)
                        {
                            Debug.LogError(compilerMessages[i].message);
                        }
                    }
                }
            };
            
            //开始构建
            if (!assemblyBuilder.Build())
            {
                Debug.LogErrorFormat("build fail：" + assemblyBuilder.assemblyPath);
                return;
            }
        }

        private static void AfterCompiling()
        {
            while (EditorApplication.isCompiling)
            {
                Debug.Log("Compiling wait1");
                // 主线程sleep并不影响编译线程
                Thread.Sleep(1000);
                Debug.Log("Compiling wait2");
            }
            
            Debug.Log("Compiling finish");

            Directory.CreateDirectory(CodeDir);
            File.Copy(Path.Combine(HotfixConfig.DllFolderPath, "Code.dll"), Path.Combine(CodeDir, "Code.dll.bytes"), true);
            File.Copy(Path.Combine(HotfixConfig.DllFolderPath, "Code.pdb"), Path.Combine(CodeDir, "Code.pdb.bytes"), true);
            AssetDatabase.Refresh();
            Debug.Log("copy Code.dll to Bundles/Code success!");
            
            // 设置ab包
            AssetImporter assetImporter1 = AssetImporter.GetAtPath("Assets/Bundles/Code/Code.dll.bytes");
            assetImporter1.assetBundleName = "Code.unity3d";
            AssetImporter assetImporter2 = AssetImporter.GetAtPath("Assets/Bundles/Code/Code.pdb.bytes");
            assetImporter2.assetBundleName = "Code.unity3d";
            AssetDatabase.Refresh();
            Debug.Log("set assetbundle success!");
            
            Debug.Log("build success!");
        }
    }
}