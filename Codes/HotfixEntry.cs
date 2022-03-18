using System;
using System.Collections.Generic;
using UnityGameFramework.Runtime;
using GameEntry = Game.GameEntry;

namespace Hotfix
{
    /// <summary>
    /// 热更新层游戏入口
    /// </summary>
    public static class HotfixEntry
    {
        /// <summary>
        /// 有限状态机
        /// </summary>
        public static FsmManager Fsm { get; private set; }

        /// <summary>
        /// 流程
        /// </summary>
        public static ProcedureManager Procedure { get; private set; }

        /// <summary>
        /// 事件
        /// </summary>
        public static EventManager Event { get; private set; }

        /// <summary>
        /// 热更层入口（非业务）
        /// </summary>
        private static void Enter()
        {
            GameEntry.Hotfix.SetLifeCircleAction(Start, Update, Shutdown, OnApplicationPause, OnApplicationQuit);
        }

        /// <summary>
        /// 热更层业务入口
        /// </summary>
        private static void Start()
        {
            Fsm = new FsmManager();
            Procedure = new ProcedureManager();
            Event = new EventManager();
            //初始化流程管理器
            var procedure = new List<ProcedureBase>();
            var typeBase = typeof(ProcedureBase);
            var types = GameEntry.Hotfix.GetAllTypes();
            foreach (var type in types)
            {
                if (type.IsClass && !type.IsAbstract && typeBase.IsAssignableFrom(type))
                {
                    procedure.Add((ProcedureBase)Activator.CreateInstance(type));
                }
            }

            Procedure.Initialize(Fsm, procedure.ToArray());

            //开始热更新层入口流程
            Procedure.StartProcedure<ProcedureEntry>();
        }

        private static void Update(float elapseSeconds, float realElapseSeconds)
        {
            Fsm.Update(elapseSeconds, realElapseSeconds);
            Event.Update(elapseSeconds, realElapseSeconds);
        }

        private static void OnApplicationPause(bool pauseStatus)
        {
            Log.Info($"Hotfix OnApplicationPause pauseStatus:{pauseStatus}");
            
        }

        private static void OnApplicationQuit()
        {
            Log.Info("Hotfix OnApplicationQuit");
            
        }

        private static void Shutdown()
        {
            Procedure.Shutdown();
            Fsm.Shutdown();
            Event.Shutdown();
        }
    }
}