using UnityEditor;
using UnityEngine;

namespace Game.Editor
{
    public class UGuiTool
    {
        private static readonly string UGuiFormTemplate = "Assets/Res/Configs/UGuiForm.prefab";
        
        [MenuItem("GameObject/UI/Form")]
        static void CreateForm()
        {
            GameObject obj = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>(UGuiFormTemplate));
            obj.name = "Form";
            obj.transform.SetParent(Selection.activeTransform);
        }
    }
}
