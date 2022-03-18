using System;

namespace Game
{
    public class HotfixLifeCircle
    {
        public Action Start { private set; get; }
        public Action<float, float> Update { private set; get; }
        public Action ShutDown { private set; get; }
        public Action<bool> OnApplicationPause { private set; get; }
        public Action OnApplicationQuit { private set; get; }

        public HotfixLifeCircle(Action start, Action<float, float> update,
            Action shutDown, Action<bool> onApplicationPause,
            Action onApplicationQuit)
        {
            Start = start;
            Update = update;
            ShutDown = shutDown;
            OnApplicationPause = onApplicationPause;
            OnApplicationQuit = onApplicationQuit;
        }
    }
}