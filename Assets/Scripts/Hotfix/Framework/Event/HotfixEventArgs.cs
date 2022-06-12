namespace Hotfix.Framework
{
    public abstract class HotfixEventArgs : IReference
    {
        private int m_Id;
        public void Clear()
        {
            m_Id = default;
        }

        public int Id => m_Id;

        public void Fill(int eventId)
        {
            m_Id = eventId;
        }
    }
}
