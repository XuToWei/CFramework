using GameFramework;

namespace Hotfix.Framework
{
    public static class AssetUtility
    {
        public static string GetLubanAsset(string assetName, bool fromBytes)
        {
            return Utility.Text.Format("Assets/Res/Luban/{0}.{1}", assetName, fromBytes ? "bytes" : "json");
        }
    }
}
