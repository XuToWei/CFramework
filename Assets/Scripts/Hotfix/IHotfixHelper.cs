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
        Task<bool> Load();
        void Enter();
        void ShutDown();
        object CreateInstance(string typeName);
        object GetMethod(string typeName,string methodName,int paramCount);
        object InvokeMethod(object method, object instance, params object[] objects);
        Type GetHotfixType(string typeName);
        List<Type> GetAllTypes();
    }
}