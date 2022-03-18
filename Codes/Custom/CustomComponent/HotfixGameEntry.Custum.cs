namespace Hotfix
{
    public partial class HotfixEntry
    {
        public HPBarComponent HpBar { get; private set; }

        private void InitCustomComponents()
        {
            HpBar = new HPBarComponent();
            HpBar.Initialize();
            UpdateEvent += HpBar.Update;
        }

        private void ShutDownCustomComponents()
        {
            UpdateEvent -= HpBar.Update;
        }
    }
}