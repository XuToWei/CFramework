using ILRuntime.CLR.TypeSystem;
using UnityEngine;

namespace Game
{
    public class HotfixEntityLogic : EntityLogic
    {
        [SerializeField] private string m_HotfixClassFullName;

        private object m_HotfixObj;
        protected override void OnInit(object userData)
        {
            base.OnInit(userData); 
            m_HotfixObj = GameEntry.Hotfix.CreateInstance(m_HotfixClassFullName);
            // using (var ctx = appdomain.BeginInvoke(method))
            // {
            //     ctx.PushObject(obj);
            //     ctx.Invoke();
            //     int id = ctx.ReadInteger();
            //     Debug.Log("!! HotFix_Project.InstanceClass.ID = " + id);
            // }
        }
    }
}
