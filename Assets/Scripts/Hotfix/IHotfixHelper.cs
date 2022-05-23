using System;
using System.Collections.Generic;
using GameFramework;
using UnityEngine;

namespace Game
{
    public interface IHotfixHelper
    {
        void LoadAssembly(byte[] dllBytes, byte[] pdbBytes);
        void Enter();
        void ShutDown();
        object CreateInstance(string typeName);
        object GetMethod(string typeName,string methodName,int paramCount);
        object InvokeMethod(object method, object instance, params object[] objects);
        T CreateHotfixMonoBehaviour<T>(GameObject go,string hotfixFullTypeName)  where T : MonoBehaviour;
        Type GetHotfixType(string typeName);
        List<Type> GetAllTypes();
        object GetHotfixGameEntry { get; }
    }
}