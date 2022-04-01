using Game;

namespace Hotfix
{
    public class ProcedureGame : ProcedureBase
    {
        protected internal override void OnEnter(IFsm procedureOwner)
        {
            base.OnEnter(procedureOwner);
            GameEntry.UI.OpenUIForm(UIFormId.Test);
        }
    }
}