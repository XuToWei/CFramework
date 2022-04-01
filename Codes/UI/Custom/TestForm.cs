using Game;
using UnityEngine;

namespace Hotfix
{
    public partial class TestForm : UGuiForm
    {
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            Debug.LogError("----------------");
        }
    }
}