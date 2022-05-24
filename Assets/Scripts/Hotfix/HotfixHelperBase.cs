using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameFramework;
using UnityEngine;

namespace Game
{
    public abstract class HotfixHelperBase : MonoBehaviour, IHotfixHelper
    {
        public abstract object HotfixGameEntry { get; }
        public abstract Task<bool> Load();
        public abstract void Enter();
        public abstract void ShutDown();
        public abstract object CreateInstance(string typeName);
        public abstract object GetMethod(string typeName, string methodName, int paramCount);
        public abstract object InvokeMethod(object method, object instance, params object[] objects);
        public abstract Type GetHotfixType(string typeName);
        public abstract List<Type> GetAllTypes();
    }
}