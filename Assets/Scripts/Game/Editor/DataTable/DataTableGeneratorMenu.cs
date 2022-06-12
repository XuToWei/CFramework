//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework;
using UnityEditor;
using UnityEngine;

namespace Game.Editor.DataTableTools
{
    public sealed class DataTableGeneratorMenu
    {
        [MenuItem("Tools/Data Table/Generate All DataTables", priority = 3)]
        public static void GenerateDataTables()
        {
            foreach (string dataTableName in ExcelExtension.ExcelToTxt())
            {
                DataTableProcessor dataTableProcessor = DataTableGenerator.CreateDataTableProcessor(dataTableName);
                if (!DataTableGenerator.CheckRawData(dataTableProcessor, dataTableName))
                {
                    Debug.LogError(Utility.Text.Format("Check raw data failure. DataTableName='{0}'", dataTableName));
                    break;
                }

                DataTableGenerator.GenerateDataFile(dataTableProcessor, dataTableName);
                DataTableGenerator.GenerateCodeFile(dataTableProcessor, dataTableName);
            }

            //生成luban配置
            LubanRunner.GenerateBin();
            Debug.Log("+++++++++++++++++++++++");
            AssetDatabase.Refresh();
        }

        [MenuItem("Tools/Data Table/Generate Builtin Data Table", priority = 3)]
        public static void GenerateBuiltinDataTable()
        {
            foreach (string dataTableName in ExcelExtension.ExcelToTxt())
            {
                DataTableProcessor dataTableProcessor = DataTableGenerator.CreateDataTableProcessor(dataTableName);
                if (!DataTableGenerator.CheckRawData(dataTableProcessor, dataTableName))
                {
                    Debug.LogError(Utility.Text.Format("Check raw data failure. DataTableName='{0}'", dataTableName));
                    break;
                }

                DataTableGenerator.GenerateDataFile(dataTableProcessor, dataTableName);
                DataTableGenerator.GenerateCodeFile(dataTableProcessor, dataTableName);
            }

            AssetDatabase.Refresh();
        }
        
        [MenuItem("Tools/Data Table/Generate Json Luban", priority = 3)]
        public static void GenerateJsonLuban()
        {
            //生成luban配置
            LubanRunner.GenerateJson();
            
            AssetDatabase.Refresh();
        }
        
        [MenuItem("Tools/Data Table/Generate Bin Luban", priority = 3)]
        public static void GenerateBinLuban()
        {
            //生成luban配置
            LubanRunner.GenerateBin();
            
            AssetDatabase.Refresh();
        }
    }
}
