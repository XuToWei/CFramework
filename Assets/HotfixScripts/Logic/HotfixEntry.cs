using System;
using UnityGameFramework.Runtime;

namespace Hotfix
{
    /// <summary>
    ///     热更新层游戏入口
    /// </summary>
    public partial class HotfixEntry
    {
        private bool IsShutDown { get; set; }

        public event Action<float, float> UpdateEvent = null;
        public event Action<bool> OnApplicationPauseEvent = null;
        public event Action OnApplicationQuitEvent = null;

        private bool IsStarted = false;
        

        public void OnEnter()
        {
            IsShutDown = false;
        }

        public void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            Log.Info($"Hotfix Update 2222222  : {elapseSeconds} {realElapseSeconds}");
        }

        public void OnApplicationPause(bool pauseStatus)
        {
            Log.Info($"Hotfix OnApplicationPause pauseStatus:{pauseStatus}");
            OnApplicationPauseEvent?.Invoke(pauseStatus);
        }

        public void OnApplicationQuit()
        {
            Log.Info("Hotfix OnApplicationQuit");
            OnApplicationQuitEvent?.Invoke();
        }

        public void OnShutDown()
        {

        }

        public void OnApplicationFocus()
        {
            
        }
    }
}
