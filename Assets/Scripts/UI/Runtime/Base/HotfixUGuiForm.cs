﻿using UnityEngine;
using GameFramework;

namespace Game
{
    /// <summary>
    /// 热更新层UGUI界面
    /// </summary>
    [DisallowMultipleComponent]
    public class HotfixUGuiForm : MonoBehaviour
    {
        /// <summary>
        /// 对应的热更新层UGUI界面类名
        /// </summary>
        [SerializeField] private string m_HotfixFormName;

        public UGuiForm GetOrAddUIFormLogic()
        {
            UGuiForm uGuiForm = gameObject.GetComponent<UGuiForm>();
            if (uGuiForm == null)
            {
                string hotfixUGuiFormFullName =
                    Utility.Text.Format("{0}.{1}", "UGFExtensions.Hotfix", m_HotfixFormName);
                uGuiForm = gameObject.AddHotfixMonoBehaviour<UGuiForm>(hotfixUGuiFormFullName);
            }

            return uGuiForm;
        }
    }
}