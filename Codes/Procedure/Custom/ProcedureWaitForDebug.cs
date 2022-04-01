using System;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Hotfix
{
    /// <summary>
    /// 等待断点进入下一流程
    /// </summary>
    public class ProcedureWaitForDebug : ProcedureBase
    {
        protected internal override void OnUpdate(IFsm procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            if (Input.GetKeyDown(KeyCode.A)) 
            {
                ChangeState<ProcedurePreload>(procedureOwner);
            }
        }
    }
}