#if ILRuntime
//using ILRuntime.Runtime.Generated;
using System.Linq;
using System.Reflection;
using AppDomain = ILRuntime.Runtime.Enviorment.AppDomain;

namespace Game
{
    public static partial class ILRuntimeUtility
    {
        public static void InitILRuntime(AppDomain appDomain)
        {
            MethodInfo[] methodInfos = typeof(ILRuntimeUtility)
                .GetMethods(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public)
                .Where(_ => _.IsDefined(typeof(ILRunTimeRegisterAttribute))).ToArray();
            object[] objects = { appDomain };
            foreach (MethodInfo methodInfo in methodInfos)
            {
                methodInfo.Invoke(null, objects);
            }
            
            //CLRBindings.Initialize(appDomain);
        }
    }
}
#endif