using System.Threading.Tasks;

namespace Game
{
    public abstract class HotfixHelperBase
    {
        public abstract HotfixType HotfixType { get; }
        public abstract Task Load();
        public abstract void Init();
        public abstract void OnEnter();
        public abstract void OnShutDown();
        public abstract void OnApplicationPause(bool pauseStatus);
        public abstract void OnApplicationFocus(bool hasFocus);
        public abstract void OnApplicationQuit();
        public abstract void OnUpdate(float elapseSeconds, float realElapseSeconds);
        public abstract object CreateInstance(string hotfixTypeFullName);
    }
}