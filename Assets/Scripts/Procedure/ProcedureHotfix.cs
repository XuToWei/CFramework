using GameFramework.Fsm;
using GameFramework.Procedure;
using ILRuntime.Runtime.Intepreter;

namespace Game
{
    public class ProcedureHotfix : ProcedureBase
    {
        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            GameEntry.Hotfix.OnEnter();
        }

        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            GameEntry.Hotfix.OnUpdate(elapseSeconds, realElapseSeconds);
        }

        protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
        {
            if (isShutdown)
            {
                GameEntry.Hotfix.OnShutDown();
            }
            base.OnLeave(procedureOwner, isShutdown);
        }
    }
}