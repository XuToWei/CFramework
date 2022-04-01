using UnityEngine;
using UnityGameFramework.Runtime;

namespace Hotfix
{
    public class ProcedureEntry : ProcedureBase
    {
        protected internal override void OnEnter(IFsm procedureOwner)
        {
            base.OnEnter(procedureOwner);
            // 编辑器下进入断点调试等待流程
            if (Application.platform == RuntimePlatform.WindowsEditor
                || Application.platform == RuntimePlatform.OSXEditor
                || Application.platform == RuntimePlatform.LinuxEditor)
            {
                ChangeState<ProcedureWaitForDebug>(procedureOwner);
            }
            else
            {
                ChangeState<ProcedurePreload>(procedureOwner);
            }
        }
    }
}