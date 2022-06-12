using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameFramework;
using GameFramework.Resource;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Game
{
    public class HotfixComponent : GameFrameworkComponent
    {
        [SerializeField] private string m_HotfixHelperTypeName;
        [SerializeField] private HotfixHelperBase m_HotfixHelper;

        public HotfixType HotfixType
        {
            private set;
            get;
        }
        
#if ILRuntime
        public ILRuntimeHotfixHelper ILRuntime
        {
            private set;
            get;
        }
#endif

        public MonoHotfixHelper Mono
        {
            private set;
            get;
        }

        private void Start()
        {
#if ILRuntime
            if (!GameEntry.Base.EditorResourceMode)
            {
                m_HotfixHelperTypeName = "Game.ILRuntimeHelper";
            }
#endif
            m_HotfixHelper = Helper.CreateHelper(m_HotfixHelperTypeName, m_HotfixHelper);
            if (m_HotfixHelper == null)
            {
                Log.Error("Can not create hotfix helper.");
                return;
            }

            HotfixType = m_HotfixHelper.HotfixType;
            m_HotfixHelper.name = "Hotfix Helper";
            Transform customHelperTrans = m_HotfixHelper.transform;
            customHelperTrans.SetParent(transform);
            customHelperTrans.localPosition = Vector3.zero;
            customHelperTrans.localScale = Vector3.one;
            switch (HotfixType)
            {
                case HotfixType.Undefined:
                    throw new GameFrameworkException("HotfixType Undefined!");
                case HotfixType.Mono:
                    Mono = m_HotfixHelper as MonoHotfixHelper;
                    break;
#if ILRuntime
                case HotfixType.ILRuntime:
                    ILRuntime = m_HotfixHelper as ILRuntimeHotfixHelper;
                    break;
#endif
            }
        }
        
        public async Task Load()
        {
            await m_HotfixHelper.Load();
        }

        public void Init()
        {
            m_HotfixHelper.Init();
        }

        public void OnEnter()
        {
            m_HotfixHelper.OnEnter();
        }
        
        public void OnShutDown()
        {
            m_HotfixHelper.OnShutDown();
        }

        public void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            m_HotfixHelper.OnUpdate(elapseSeconds, realElapseSeconds);
        }

        private void OnApplicationQuit()
        {
            m_HotfixHelper?.OnApplicationQuit();
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            m_HotfixHelper?.OnApplicationPause(pauseStatus);
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            m_HotfixHelper?.OnApplicationPause(hasFocus);
        }

#if UNITY_EDITOR

        public void Reload()
        {
            if (m_HotfixHelper is not MonoHotfixHelper mono)
            {
                throw new GameFrameworkException(Utility.Text.Format("[0] can't reload, can use Game.MonoHelper to reload!", m_HotfixHelperTypeName));
            }
            mono.Reload();
        }
#endif
    }
}