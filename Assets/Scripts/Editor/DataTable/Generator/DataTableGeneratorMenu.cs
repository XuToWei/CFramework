using System.IO;
using GameFramework;
using OfficeOpenXml;
using UnityEditor;
using UnityEngine;

namespace Game.Editor.DataTable
{
    public sealed class DataTableGeneratorMenu
    {
        [MenuItem("Tools/DataTable/Generate DataTables From Txt", priority= 3)]
        public static void GenerateDataTablesFromTxt()
        {
            DataTableConfig.RefreshDataTables();
            ExtensionsGenerate.GenerateExtensionByAnalysis(ExtensionsGenerate.DataTableType.Txt,DataTableConfig.TxtFilePaths, 2);
            foreach (var dataTableName in DataTableConfig.DataTableNames)
            {
                var dataTableProcessor = DataTableGenerator.CreateDataTableProcessor(dataTableName);
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

        [MenuItem("Tools/DataTable/Generate DataTables From Excel", priority= 2)]
        public static void GenerateDataTablesFormExcel()
        {
            DataTableConfig.RefreshDataTables();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExtensionsGenerate.GenerateExtensionByAnalysis(ExtensionsGenerate.DataTableType.Excel,DataTableConfig.ExcelFilePaths, 2);
            foreach (var excelFile in DataTableConfig.ExcelFilePaths)
            {
                using (FileStream fileStream =
                    new FileStream(excelFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (ExcelPackage excelPackage = new ExcelPackage(fileStream))
                    {
                        for (int i = 0; i < excelPackage.Workbook.Worksheets.Count; i++)
                        {
                            ExcelWorksheet sheet = excelPackage.Workbook.Worksheets[i];
                            var dataTableProcessor = DataTableGenerator.CreateExcelDataTableProcessor(sheet);
                            if (!DataTableGenerator.CheckRawData(dataTableProcessor, sheet.Name))
                            {
                                Debug.LogError(Utility.Text.Format("Check raw data failure. DataTableName='{0}'",
                                    sheet.Name));
                                break;
                            }

                            DataTableGenerator.GenerateDataFile(dataTableProcessor, sheet.Name);
                            DataTableGenerator.GenerateCodeFile(dataTableProcessor, sheet.Name);
                        }
                    }
                }
            }

            AssetDatabase.Refresh();
        }
        
        [MenuItem("Tools/DataTable/Generate All DataTables From Txt", priority = 1)]
        public static void GenerateAllDataTablesFormTxt()
        {
            GenerateDataTablesFromTxt();
            HotfixDataTableGeneratorMenu.GenerateDataTablesFromTxt();
        }

        [MenuItem("Tools/DataTable/Generate All DataTables From Excel", priority = 0)]
        public static void GenerateAllDataTablesFromExcel()
        {
            GenerateDataTablesFormExcel();
            HotfixDataTableGeneratorMenu.GenerateDataTablesFromExcel();
        }
    }
}