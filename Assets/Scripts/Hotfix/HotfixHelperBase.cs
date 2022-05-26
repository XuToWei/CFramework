using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Game
{
    public abstract class HotfixHelperBase : MonoBehaviour, IHotfixHelper
    {
        public abstract object HotfixGameEntry { get; }
        public abstract Task Load();
        public abstract void OnEnter();
        public abstract void OnShutDown();
        public abstract void OnUpdate(float elapseSeconds, float realElapseSeconds);
        public abstract object CreateInstance(string typeName);
        public abstract object GetMethod(string typeName, string methodName, int paramCount);
        public abstract object InvokeMethod(object method, object instance, params object[] objects);
        public abstract Type GetHotfixType(string typeName);
    }
}