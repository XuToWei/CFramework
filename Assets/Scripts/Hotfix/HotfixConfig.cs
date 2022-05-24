namespace Game
{
    public static class HotfixConfig
    {
        public const string DllFolderPath = "Assets/Res/HotfixDlls/";

        public static readonly string[] DllNames = new[]
        {
            "Hotfix.Framework",
            "Hotfix.Logic",
            "Hotfix.LogicView",
            "Hotfix.Model",
            "Hotfix.ModelView"
        };

        public static readonly string EntryTypeFullName = "Hotfix.HotfixEntry";
    }
}

