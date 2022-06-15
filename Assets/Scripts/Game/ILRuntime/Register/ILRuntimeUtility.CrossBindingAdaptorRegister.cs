using ILRuntime.Runtime.Enviorment;

namespace Game
{
    public static partial class ILRuntimeUtility
    {
        [ILRunTimeRegister(ILRegister.Adaptor)]
        public static void RegisterCrossBindingAdaptor(AppDomain appDomain)
        {
            //注册跨域继承适配器
            appDomain.RegisterCrossBindingAdaptor(new IAsyncStateMachineAdapter());
            appDomain.RegisterCrossBindingAdaptor(new CoroutineAdapter());
        }
    }
}