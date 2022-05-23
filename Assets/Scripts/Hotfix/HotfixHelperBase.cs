using System;
using System.Collections.Generic;
using GameFramework;
using UnityEngine;

namespace Game
{
    public abstract class HotfixHelperBase : MonoBehaviour, IHotfixHelper
    {
        public abstract void LoadAssembly(byte[] dllBytes, byte[] pdbBytes);
        public abstract void Enter();
        public abstract void ShutDown();
        public abstract object CreateInstance(string typeName);
        public abstract object GetMethod(string typeName, string methodName, int paramCount);
        public abstract object InvokeMethod(object method, object instance, params object[] objects);
        public abstract T CreateHotfixMonoBehaviour<T>(GameObject go, string hotfixFullTypeName) where T : MonoBehaviour;
        public abstract Type GetHotfixType(string typeName);
        public abstract List<Type> GetAllTypes();
        public abstract object GetHotfixGameEntry { get; }
    }
}