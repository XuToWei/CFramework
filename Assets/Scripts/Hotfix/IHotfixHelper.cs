using System.Threading.Tasks;

namespace Game
{
    internal interface IHotfixHelper
    {
        HotfixType HotfixType { get; }
        Task Load();
        void Init();
        void OnEnter();
        void OnShutDown();
        void OnApplicationPause(bool pauseStatus);
        void OnApplicationFocus(bool hasFocus);
        void OnApplicationQuit();
        void OnUpdate(float elapseSeconds, float realElapseSeconds);
    }
}