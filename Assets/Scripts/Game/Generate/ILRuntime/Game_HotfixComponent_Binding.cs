using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

using ILRuntime.CLR.TypeSystem;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using ILRuntime.Runtime.Stack;
using ILRuntime.Reflection;
using ILRuntime.CLR.Utils;

namespace ILRuntime.Runtime.Generated
{
    unsafe class Game_HotfixComponent_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            MethodBase method;
            Type[] args;
            Type type = typeof(Game.HotfixComponent);
            args = new Type[]{typeof(System.Action), typeof(System.Action<System.Single, System.Single>), typeof(System.Action), typeof(System.Action<System.Boolean>), typeof(System.Action)};
            method = type.GetMethod("SetLifeCircleAction", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, SetLifeCircleAction_0);
            args = new Type[]{};
            method = type.GetMethod("GetAllTypes", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, GetAllTypes_1);


        }


        static StackObject* SetLifeCircleAction_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 6);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Action @onApplicationQuit = (System.Action)typeof(System.Action).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)8);
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.Action<System.Boolean> @onApplicationPause = (System.Action<System.Boolean>)typeof(System.Action<System.Boolean>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)8);
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            System.Action @shutDown = (System.Action)typeof(System.Action).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)8);
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 4);
            System.Action<System.Single, System.Single> @update = (System.Action<System.Single, System.Single>)typeof(System.Action<System.Single, System.Single>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)8);
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 5);
            System.Action @start = (System.Action)typeof(System.Action).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)8);
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 6);
            Game.HotfixComponent instance_of_this_method = (Game.HotfixComponent)typeof(Game.HotfixComponent).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

                //instance_of_this_method.SetLifeCircleAction(@start, @update, @shutDown, @onApplicationPause, @onApplicationQuit);

            return __ret;
        }

        static StackObject* GetAllTypes_1(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            Game.HotfixComponent instance_of_this_method = (Game.HotfixComponent)typeof(Game.HotfixComponent).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            __intp.Free(ptr_of_this_method);

           

            return ILIntepreter.PushObject(__ret, __mStack, null);
        }



    }
}
