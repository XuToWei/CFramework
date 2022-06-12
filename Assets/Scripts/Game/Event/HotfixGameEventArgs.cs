using GameFramework.Event;

namespace Game
{
    public class HotfixGameEventArgs : GameEventArgs
    {
        private int m_Id;

        public object HotfixEventArgs
        {
            get;
            private set;
        }
        
        public override void Clear()
        {
            m_Id = default;
            HotfixEventArgs = default;
        }

        public override int Id => m_Id;

        public void Fill(int eventId, object hotfixEventArgs)
        {
            m_Id = eventId;
            HotfixEventArgs = hotfixEventArgs;
        }
    }
}
