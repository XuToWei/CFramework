namespace Game
{
    public static class HotfixConfig
    {
        public const string DllFolderPath = "Assets/Res/HotfixDlls/";

        public static readonly string[] DllNames = {
            "Hotfix.Framework",
            "Hotfix.Logic",
            "Hotfix.LogicView",
            "Hotfix.Model",
            "Hotfix.ModelView"
        };

        public static readonly string[] ReloadDllNames = {
            "Hotfix.Logic",
            "Hotfix.LogicView",
        };

        public static readonly string EntryTypeFullName = "Hotfix.Logic.HotfixEntry";
    }
}

