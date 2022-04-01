using UnityEngine;

namespace Game
{
    public static class UnityExtension
    {
        public static T AddHotfixMonoBehaviour<T>(this GameObject go, string hotfixFullTypeName) where T : MonoBehaviour
        {
            return GameEntry.Hotfix.AddHotfixMonoBehaviour<T>(go, hotfixFullTypeName);
        }
    }
}