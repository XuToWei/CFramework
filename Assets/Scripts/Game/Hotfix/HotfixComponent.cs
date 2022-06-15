using System.Threading.Tasks;
using GameFramework;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Game
{
    public class HotfixComponent : GameFrameworkComponent
    {
        [SerializeField] private HotfixType m_HotfixType;

        public HotfixType HotfixType => m_HotfixType;
        
        private HotfixHelperBase m_HotfixHelper;

        public HotfixHelperBase HotfixHelper => m_HotfixHelper;

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
        
#if UNITY_EDITOR
        public EditorHotfixHelper Editor
        {
            private set;
            get;
        }
#endif

        private void Start()
        {
#if ILRuntime
            if (!GameEntry.Base.EditorResourceMode)
            {
                m_HotfixType = HotfixType.ILRuntime;
            }
#endif

            switch (HotfixType)
            {
                case HotfixType.Undefined:
                    throw new GameFrameworkException("HotfixType Undefined!");
                case HotfixType.Mono:
                    Mono = new MonoHotfixHelper();
                    m_HotfixHelper = Mono;
                    break;
#if ILRuntime
                case HotfixType.ILRuntime:
                    ILRuntime = new ILRuntimeHotfixHelper();
                    m_HotfixHelper = ILRuntime;
                    break;
#endif
#if UNITY_EDITOR
                case HotfixType.Editor:
                    Editor = new EditorHotfixHelper();
                    m_HotfixHelper = Editor;
                    break;
#endif
            }
        }

        public object CreateInstance(string hotfixTypeFullName)
        {
            return m_HotfixHelper.CreateInstance(hotfixTypeFullName);
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
            m_HotfixHelper?.OnApplicationFocus(hasFocus);
        }

#if UNITY_EDITOR
        public void Reload()
        {
            if (m_HotfixType != HotfixType.Mono)
            {
                throw new GameFrameworkException(Utility.Text.Format("[0] can't reload, can use Game.MonoHelper to reload!", m_HotfixType));
            }
            Mono.Reload();
        }
#endif
    }
}