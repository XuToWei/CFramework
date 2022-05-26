using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameFramework;
using UnityEngine;

namespace Game
{
    public interface IHotfixHelper
    {
        object HotfixGameEntry { get; }
        Task Load();
        void OnEnter();
        void OnShutDown();
        void OnUpdate(float elapseSeconds, float realElapseSeconds);
        object CreateInstance(string typeName);
        object GetMethod(string typeName,string methodName,int paramCount);
        object InvokeMethod(object method, object instance, params object[] objects);
        Type GetHotfixType(string typeName);
    }
}