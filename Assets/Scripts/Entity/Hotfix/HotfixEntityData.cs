using GameFramework;

namespace Game
{
    internal class HotfixEntityData : IReference
    {
        public string HotfixType
        {
            private set;
            get;
        }

        public object UserData
        {
            private set;
            get;
        }
        
        public void Clear()
        {
            HotfixType = default;
        }

        public void Fill(string hotfixType, object userData)
        {
            HotfixType = hotfixType;
            UserData = userData;
        }
    }
}
