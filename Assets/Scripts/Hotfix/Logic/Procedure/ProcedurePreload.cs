using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bright.Serialization;
using UnityEngine;
using UnityGameFramework.Runtime;
using ProcedureOwner = Hotfix.Framework.IFsm<Hotfix.Framework.ProcedureManager>;
using Game;
using GameEntry = Game.GameEntry;
using GameFramework.Event;
using GameFramework;
using SimpleJSON;
using Hotfix.Model;

namespace Hotfix.Logic
{
    public class ProcedurePreload : Hotfix.Framework.ProcedureBase
    {
        private static readonly List<string> DataTableNames = new List<string>()
        {
            "UIForm"
        };

        private readonly GameFrameworkLinkedList<string> m_LoadedFlags = new GameFrameworkLinkedList<string>();

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);

            GameEntry.Event.Subscribe(LoadConfigSuccessEventArgs.EventId, OnLoadConfigSuccess);
            GameEntry.Event.Subscribe(LoadConfigFailureEventArgs.EventId, OnLoadConfigFailure);
            GameEntry.Event.Subscribe(LoadDictionarySuccessEventArgs.EventId, OnLoadDictionarySuccess);
            GameEntry.Event.Subscribe(LoadDictionaryFailureEventArgs.EventId, OnLoadDictionaryFailure);
            GameEntry.Event.Subscribe(LoadDataTableSuccessEventArgs.EventId, OnLoadDataTableSuccess);
            GameEntry.Event.Subscribe(LoadDataTableFailureEventArgs.EventId, OnLoadDataTableFailure);

            m_LoadedFlags.Clear();

            PreloadResources();
        }

        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            GameEntry.Event.Unsubscribe(LoadConfigSuccessEventArgs.EventId, OnLoadConfigSuccess);
            GameEntry.Event.Unsubscribe(LoadConfigFailureEventArgs.EventId, OnLoadConfigFailure);
            GameEntry.Event.Unsubscribe(LoadDictionarySuccessEventArgs.EventId, OnLoadDictionarySuccess);
            GameEntry.Event.Unsubscribe(LoadDictionaryFailureEventArgs.EventId, OnLoadDictionaryFailure);
            GameEntry.Event.Unsubscribe(LoadDataTableSuccessEventArgs.EventId, OnLoadDataTableSuccess);
            GameEntry.Event.Unsubscribe(LoadDataTableFailureEventArgs.EventId, OnLoadDataTableFailure);

            base.OnLeave(procedureOwner, isShutdown);
        }

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds,
            float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            if (m_LoadedFlags.Count > 0)
            {
                return;
            }
            
            //ChangeState<ProcedureGame>(procedureOwner);
        }

        private void PreloadResources()
        {
            // Preload configs
            // LoadConfig("DefaultConfig");

            // Preload data tables
            foreach (string dataTableName in DataTableNames)
            {
                LoadDataTable(dataTableName);
            }

            PreloadLuban(typeof(Model.Tables), "Hotfix");
            PreloadLuban(typeof(Game.Tables), "Game");

            // Preload dictionaries
            // LoadDictionary("Default");

            // Preload fonts
            // LoadFont("MainFont");
        }

        private async void PreloadLuban(Type tablesType, string lubanType)
        {
            m_LoadedFlags.AddLast(tablesType.FullName);
            
            var tablesCtor = tablesType.GetMethod("LoadAsync");
            
            var loaderReturnType = tablesCtor.GetParameters()[0].ParameterType.GetGenericArguments()[1];
            
            Debug.Log($"{tablesType} : {tablesCtor} -- {loaderReturnType}");
            // 根据cfg.Tables的构造函数的Loader的返回值类型决定使用json还是ByteBuf Loader
            async Task<JSONNode> LoadJson(string file)
            {
                TextAsset textAsset = await GameEntry.Resource.LoadAssetAsync<TextAsset>(Framework.AssetUtility.GetLubanAsset(file, lubanType, true));
                return JSON.Parse(textAsset.text);
            }
            async Task<ByteBuf> LoadByteBuf(string file)
            {
                TextAsset textAsset = await GameEntry.Resource.LoadAssetAsync<TextAsset>(Framework.AssetUtility.GetLubanAsset(file, lubanType, true));
                return new ByteBuf(textAsset.bytes);
            }
            Delegate loader = loaderReturnType == typeof(Task<ByteBuf>) ? new Func<string, Task<ByteBuf>>(LoadByteBuf) : new Func<string, Task<JSONNode>>(LoadJson);
            var tables = Activator.CreateInstance(tablesType);

            var task = (Task)tablesCtor.Invoke(tables, new object[] {loader});
            await task;
            m_LoadedFlags.Remove(tablesType.FullName);
        }

        private void LoadConfig(string configName)
        {
            string configAssetName = AssetUtility.GetConfigAsset(configName, false);
            m_LoadedFlags.AddLast(configAssetName);
            GameEntry.Config.ReadData(configAssetName, this);
        }

        private void LoadDataTable(string dataTableName)
        {
            string dataTableAssetName = AssetUtility.GetDataTableAsset(dataTableName, true);
            m_LoadedFlags.AddLast(dataTableAssetName);
            GameEntry.DataTable.LoadDataTable(dataTableName, dataTableAssetName, null);
            Log.Info("Load data table '{0}' row config OK.", dataTableName);
        }

        private void LoadDictionary(string dictionaryName)
        {
            string dictionaryAssetName = AssetUtility.GetDictionaryAsset(dictionaryName, false);
            m_LoadedFlags.AddLast(dictionaryAssetName);
            GameEntry.Localization.ReadData(dictionaryAssetName, this);
        }

        private void OnLoadConfigSuccess(object sender, GameEventArgs e)
        {
            LoadConfigSuccessEventArgs ne = (LoadConfigSuccessEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }

            m_LoadedFlags.Remove(ne.ConfigAssetName);
            Log.Info("Load config '{0}' OK.", ne.ConfigAssetName);
        }

        private void OnLoadConfigFailure(object sender, GameEventArgs e)
        {
            LoadConfigFailureEventArgs ne = (LoadConfigFailureEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }

            Log.Error("Can not load config '{0}' from '{1}' with error message '{2}'.", ne.ConfigAssetName,
                ne.ConfigAssetName, ne.ErrorMessage);
        }

        private void OnLoadDictionarySuccess(object sender, GameEventArgs e)
        {
            LoadDictionarySuccessEventArgs ne = (LoadDictionarySuccessEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }

            m_LoadedFlags.Remove(ne.DictionaryAssetName);
            Log.Info("Load dictionary '{0}' OK.", ne.DictionaryAssetName);
        }

        private void OnLoadDictionaryFailure(object sender, GameEventArgs e)
        {
            LoadDictionaryFailureEventArgs ne = (LoadDictionaryFailureEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }

            Log.Error("Can not load dictionary '{0}' from '{1}' with error message '{2}'.", ne.DictionaryAssetName,
                ne.DictionaryAssetName, ne.ErrorMessage);
        }
        
        private void OnLoadDataTableSuccess(object sender, GameEventArgs e)
        {
            LoadDataTableSuccessEventArgs ne = (LoadDataTableSuccessEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }

            m_LoadedFlags.Remove(ne.DataTableAssetName);
            Log.Info("Load data table '{0}' OK.", ne.DataTableAssetName);
        }

        private void OnLoadDataTableFailure(object sender, GameEventArgs e)
        {
            LoadDataTableFailureEventArgs ne = (LoadDataTableFailureEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }

            Log.Error("Can not load data table '{0}' from '{1}' with error message '{2}'.", ne.DataTableAssetName, ne.DataTableAssetName, ne.ErrorMessage);
        }
    }
}