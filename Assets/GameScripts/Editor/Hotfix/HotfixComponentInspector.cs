using UnityEditor;
using UnityGameFramework.Editor;

namespace Game.Editor.Hotfix
{
    [CustomEditor(typeof(HotfixComponent))]
    public class HotfixComponentInspector : GameFrameworkInspector
    {
        private HelperInfo<BaseHotfixHelper> m_HotfixHelperInfo = new HelperInfo<BaseHotfixHelper>("Hotfix");
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            serializedObject.Update();

            HotfixComponent t = (HotfixComponent)target;
            
            EditorGUI.BeginDisabledGroup(EditorApplication.isPlayingOrWillChangePlaymode);
            {
                m_HotfixHelperInfo.Draw();
            }
            EditorGUI.EndDisabledGroup();
            
            serializedObject.ApplyModifiedProperties();

            Repaint();
        }
        
        private void OnEnable()
        {
            m_HotfixHelperInfo.Init(serializedObject);

            RefreshTypeNames();
        }
        
        protected override void OnCompileComplete()
        {
            base.OnCompileComplete();

            RefreshTypeNames();
        }
        
        private void RefreshTypeNames()
        {
            m_HotfixHelperInfo.Refresh();
            serializedObject.ApplyModifiedProperties();
        }
    }
}