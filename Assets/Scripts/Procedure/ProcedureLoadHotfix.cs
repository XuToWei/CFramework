using GameFramework.Fsm;
using GameFramework.Procedure;

namespace Game
{
    public class ProcedureLoadHotfix : ProcedureBase
    {
        private bool m_IsLoaded;
        
        // ReSharper disable Unity.PerformanceAnalysis
        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            m_IsLoaded = false;
            GameEntry.Hotfix.Load();
        }

        private void OnLoadHotfixCompleted()
        {
            m_IsLoaded = true;
        }

        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            if (m_IsLoaded)
            {
                ChangeState<ProcedureHotfix>(procedureOwner);
            }
        }
    }
}
